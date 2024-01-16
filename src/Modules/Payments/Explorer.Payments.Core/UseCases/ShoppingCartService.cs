using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingSession;
using Explorer.Stakeholders.API.Internal;
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
    private readonly ITouristWalletRepository _walletRepository;
    private readonly IInternalNotificationService _internalNotificationService;

    public ShoppingCartService(IShoppingCartRepository repository, IItemRepository itemRepository, ISaleRepository saleRepository, 
        ICouponRepository couponRepository, ITourPurchaseTokenRepository purchaseTokenRepository, ICrudRepository<PaymentRecord> paymentRecordRepository, 
        ITouristWalletRepository walletRepository, IInternalNotificationService internalNotificationService, IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
        _shoppingCartRepository = repository;
        _itemRepository = itemRepository;
        _saleRepository = saleRepository;
        _couponRepository = couponRepository;
        _purchaseTokenRepository = purchaseTokenRepository;
        _paymentRecordRepository = paymentRecordRepository;
        _walletRepository = walletRepository;
        _internalNotificationService = internalNotificationService;
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

    public Result<CouponDto> GetByCouponText(string couponText)
    {
        try
        {
            var result = _shoppingCartRepository.GetByCouponText(couponText);
            CouponDto dto = new CouponDto();
            dto.Code = couponText;
            dto.SellerId = result.SellerId;
            dto.Id = (int)result.Id;
            dto.DiscountPercentage = result.DiscountPercentage;
            dto.ExpirationDate = result.ExpirationDate;
            dto.IsGlobal = result.IsGlobal;
            dto.TourId = result.TourId;
            return dto;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result StartSession(int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.GetByUser(userId);

            if (cart.HasExpiredSession())
            {
                cart.LeavePreviousSession();
            }

            if (!cart.HasActiveSession())
            {
                cart.StartShoppingSession();
            }

            _shoppingCartRepository.Update(cart);

            return Result.Ok();
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

            var hasPurchased = _purchaseTokenRepository.HasPurchasedTour(orderItem.ItemId, userId);
            if (hasPurchased) throw new ArgumentException("Tour has already been purchased.");

            _itemRepository.GetByItemIdAndType(orderItem.ItemId, orderItem.Type);

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

    public Result<ShoppingCartDto> CheckOut(long userId, string? couponCode)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByUser(userId);
            if (shoppingCart.IsEmpty()) throw new ArgumentException("Can't proceed, shopping cart is empty!");

            var wallet = _walletRepository.GetByUser(userId);

            UpdateShoppingCart(shoppingCart, true);

            var purchasedItems = GetPurchasedItems(shoppingCart);

            ApplySale(purchasedItems);
            ApplyCoupon(shoppingCart, purchasedItems, couponCode);

            Purchase(wallet, purchasedItems);

            CreatePaymentRecords(userId, purchasedItems);
            CreatePurchaseTokens(userId, purchasedItems);

            shoppingCart.CheckOut();
            var result = _shoppingCartRepository.Update(shoppingCart);

            SendNotification(userId, purchasedItems);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Result.Fail(FailureCode.PaymentRequired).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private static void Purchase(TouristWallet wallet, List<Item> purchasedItems)
    {
        var total = purchasedItems.Sum(i => i.Price);
        wallet.Pay(total);
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

    private void ApplyCoupon(ShoppingCart shoppingCart, List<Item> purchasedItems, string? couponCode)
    {
        if (couponCode == null || string.IsNullOrWhiteSpace(couponCode)) return;

        var coupon = _couponRepository.GetByCode(couponCode);
        var tourItems = purchasedItems.Where(i => i.Type == ItemType.Tour).ToList();

        var discountedTourId = coupon.Apply(tourItems);
        shoppingCart.UseCoupon(coupon.Id, discountedTourId);
    }

    private List<Item> GetPurchasedItems(ShoppingCart shoppingCart)
    {
        var purchasedItems = new List<Item>();
        foreach (var orderItem in shoppingCart.Items)
        {
            var item = _itemRepository.GetByItemIdAndType(orderItem.ItemId, orderItem.Type);
            if (item is BundleItem bundleItem)
                purchasedItems.Add(new BundleItem(bundleItem));
            else
                purchasedItems.Add(new Item(item));
        }

        return purchasedItems;
    }

    private void UpdateShoppingCart(ShoppingCart shoppingCart, bool isCheckout = false)
    {
        var items = shoppingCart.Items.ToList();
        foreach (var orderItem in items)
        {
            var item = _itemRepository.GetByItemIdAndType(orderItem.ItemId, orderItem.Type);
            var updatedItem = CheckSale(item);

            if (isCheckout && orderItem.Price != updatedItem.Price)
                throw new ArgumentException(
                    $"Pricing mismatch for item {orderItem.Name}: Cart price: {orderItem.Price}, Current price: {updatedItem.Price}");

            shoppingCart.UpdateItem(orderItem, updatedItem);
        }

        _shoppingCartRepository.Update(shoppingCart);
    }

    private Item CheckSale(Item item)
    {
        var itemOnSale = new Item(item);
        if (item.Type != ItemType.Tour) return itemOnSale;

        var sales = _saleRepository.GetActiveSalesForTour(item.ItemId);
        var salePrice = sales.Aggregate(itemOnSale.Price, (currentPrice, sale) => sale.ApplyDiscount(currentPrice));
        itemOnSale.UpdatePrice(salePrice);

        return itemOnSale;
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

    private void SendNotification(long userId, List<Item> purchasedItems)
    {
        var description = "Congratulations on your successful purchase! Here are the details of your purchase:\n";
        foreach (var item in purchasedItems)
        {
            description += $"\t- {item.Name}: {item.Price} AC\n";
        }
        description += $"\nTotal Amount Spent: {purchasedItems.Sum(item => item.Price)} AC\n";
        description += "You can view your newly purchased tours on your Purchased Tours page.\n";

        _internalNotificationService.CreatePaymentNotification(description, userId);
    }
}

