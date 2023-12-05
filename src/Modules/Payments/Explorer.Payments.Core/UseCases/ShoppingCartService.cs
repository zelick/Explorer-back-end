using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
{
    private readonly IMapper _mapper;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IItemRepository _itemRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly ITourPurchaseTokenRepository _purchaseTokenRepository;
    private readonly ICrudRepository<PaymentRecord> _paymentRecordRepository;

    public ShoppingCartService(IShoppingCartRepository repository, IItemRepository itemRepository,
        ISaleRepository saleRepository, ICouponRepository couponRepository,
        ITourPurchaseTokenRepository purchaseTokenRepository, ICrudRepository<PaymentRecord> paymentRecordRepository, IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
        _shoppingCartRepository = repository;
        _itemRepository = itemRepository;
        _saleRepository = saleRepository;
        _couponRepository = couponRepository;
        _purchaseTokenRepository = purchaseTokenRepository;
        _paymentRecordRepository = paymentRecordRepository;
    }

    public Result<ShoppingCartDto> GetByUser(long userId)
    {
        try
        {
            var result = _shoppingCartRepository.GetByUser(userId);

            UpdateShoppingCart(result);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<ShoppingCartDto> AddItem(ItemDto orderItemDto, int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.GetByUser(userId);

            var orderItem = _mapper.Map<ItemDto, OrderItem>(orderItemDto);

            var item = _itemRepository.GetByItemIdAndType(orderItem.ItemId, orderItem.Type);
            if (item.Price != orderItem.Price) throw new ArgumentException("Item price is invalid.");

            cart.AddItem(orderItem);

            var result = _shoppingCartRepository.Update(cart);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<ShoppingCartDto> RemoveItem(ItemDto orderItemDto, int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.GetByUser(userId);

            var orderItem = _mapper.Map<ItemDto, OrderItem>(orderItemDto);
            cart.RemoveItem(orderItem);

            var result = _shoppingCartRepository.Update(cart);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<ShoppingCartDto> CheckOut(long userId, string couponCode)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByUser(userId);
            UpdateShoppingCart(shoppingCart, true);

            var purchasedItems = GetPurchasedItems(shoppingCart);

            ApplySale(purchasedItems);
            ApplyCoupon(purchasedItems, couponCode);

            var total = purchasedItems.Sum(i => i.Price);
            // TODO 1: check wallet balance and deduct
            // if (total > 0) return Result.Fail(FailureCode.PaymentRequired);

            CreatePaymentRecords(userId, purchasedItems);
            CreatePurchaseTokens(userId, purchasedItems);

            shoppingCart.CheckOut();
            var result = _shoppingCartRepository.Update(shoppingCart);

            // TODO 2: send notification

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Result.Fail(FailureCode.Conflict).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private void ApplySale(List<Item> purchasedItems)
    {
        var tourItems = purchasedItems.Where(i => i.Type == ItemType.Tour);
        foreach (var item in tourItems)
        {
            var sales = _saleRepository.GetActiveSalesForTour(item.ItemId);
            var finalDiscountedPrice = sales.Aggregate(item.Price, (currentPrice, sale) => sale.ApplyDiscount(currentPrice));

            item.UpdatePrice(finalDiscountedPrice);
        }
    }


    private void ApplyCoupon(List<Item> purchasedItems, string couponCode)
    {
        if (string.IsNullOrWhiteSpace(couponCode)) return;

        var coupon = _couponRepository.GetByCode(couponCode);
        var tourItems = purchasedItems.Where(i => i.Type == ItemType.Tour).ToList();

        coupon.Apply(tourItems);
    }

    private List<Item> GetPurchasedItems(ShoppingCart shoppingCart)
    {
        var purchasedItems = new List<Item>();
        foreach (var orderItem in shoppingCart.Items)
        {
            var item = _itemRepository.GetByItemIdAndType(orderItem.ItemId, orderItem.Type);
            purchasedItems.Add(item);
        }

        return purchasedItems;
    }

    private void UpdateShoppingCart(ShoppingCart shoppingCart, bool isCheckout = false)
    {
        var items = shoppingCart.Items.ToList();
        foreach (var orderItem in items)
        {
            var updatedItem = _itemRepository.GetByItemIdAndType(orderItem.ItemId, orderItem.Type);

            if (isCheckout && orderItem.Price != updatedItem.Price)
                throw new InvalidOperationException(
                    $"Pricing mismatch for item {orderItem.Name}: Cart price: {orderItem.Price}, Current price: {updatedItem.Price}");

            shoppingCart.UpdateItem(orderItem, updatedItem);
        }

        _shoppingCartRepository.Update(shoppingCart);
    }


    private void CreatePaymentRecords(long userId, List<Item> purchasedItems)
    {
        foreach (var item in purchasedItems)
        {
            var paymentRecord = new PaymentRecord(userId, item.Id, item.Price);
            _paymentRecordRepository.Create(paymentRecord);
        }
    }

    private void CreatePurchaseTokens(long userId, List<Item> purchasedItems)
    {

        var purchasedTourIds = GetPurchasedTourIds(purchasedItems);
        foreach (var tourId in purchasedTourIds)
        {
            var purchaseToken = new TourPurchaseToken(userId, tourId);
            _purchaseTokenRepository.Create(purchaseToken);
        }
    }

    private List<long> GetPurchasedTourIds(List<Item> purchasedItems)
    {
        var purchasedTourIds = new List<long>();
        foreach (var item in purchasedItems)
        {
            if (item.Type == ItemType.Tour)
            {
                purchasedTourIds.Add(item.ItemId);
            }
            else if (item is BundleItem bundleItem)
            {
                purchasedTourIds.AddRange(bundleItem.BundleItemIds);
            }
        }

        return purchasedTourIds;
    }
}

