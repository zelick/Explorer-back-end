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
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ICustomerRepository _customerRepository;

    public ShoppingCartService(IShoppingCartRepository repository, ICustomerRepository customerRepository, IMapper mapper) : base(mapper)
    {
        _shoppingCartRepository = repository;
        _customerRepository = customerRepository;
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

    public Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCartDto, int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.Get(shoppingCartDto.Id);

            if (!cart.IsOwnedByUser(userId))
                throw new InvalidOperationException("Only the user whose cart it is can update it.");

            var shoppingCart = MapToDomain(shoppingCartDto);
            shoppingCart.CalculateTotalPrice();

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

    public Result<ShoppingCartDto> CheckOut(long userId)
    {
        try
        {
            var customer = _customerRepository.GetByUser(userId);
            var shoppingCart = _shoppingCartRepository.GetByUser(userId);

            var purchasedTourIds = shoppingCart.CheckOut();
            customer.AddTourPurchaseTokens(purchasedTourIds);

            _customerRepository.Update(customer);
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

