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
    private readonly ITourPurchaseTokenRepository _purchaseTokenRepository;

    public ShoppingCartService(IShoppingCartRepository repository, ITourPurchaseTokenRepository purchaseTokenRepository, IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
        _shoppingCartRepository = repository;
        _purchaseTokenRepository = purchaseTokenRepository;
    }

    public Result<ShoppingCartDto> GetByUser(long userId)
    {
        try
        {
            var result = _shoppingCartRepository.GetByUser(userId);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<ShoppingCartDto> AddItem(OrderItemDto orderItemDto, int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.GetByUser(userId);

            var item = _mapper.Map<OrderItemDto, OrderItem>(orderItemDto);
            cart.AddItem(item);

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

    public Result<ShoppingCartDto> RemoveItem(OrderItemDto orderItemDto, int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.GetByUser(userId);

            var item = _mapper.Map<OrderItemDto, OrderItem>(orderItemDto);
            cart.RemoveItem(item);

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

    public Result<ShoppingCartDto> CheckOut(long userId)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByUser(userId);

            // todo should be in payment service
            var purchasedTourIds = shoppingCart.CheckOut();
            foreach (var tourId in purchasedTourIds)
            {
                _purchaseTokenRepository.Create(new TourPurchaseToken(userId, tourId));
            }
            
            var result = _shoppingCartRepository.Update(shoppingCart);

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
}

