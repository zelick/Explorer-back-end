using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class CustomerService : BaseService<CustomerDto, Customer>, ICustomerService, IInternalShoppingService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public CustomerService(ICustomerRepository repository, IShoppingCartRepository shoppingCartRepository, IMapper mapper) : base(mapper)
    {
        _customerRepository = repository;
        _shoppingCartRepository = shoppingCartRepository;
    }

    public Result<CustomerDto> GetByUser(long userId)
    {
        try
        {
            var result = _customerRepository.GetByUser(userId);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<CustomerDto> Create(CustomerDto customerDto)
    {
        try
        {
            var shoppingCart = new ShoppingCart(customerDto.UserId);
            var newShoppingCart = _shoppingCartRepository.Create(shoppingCart);

            customerDto.ShoppingCartId = newShoppingCart.Id;

            var result = _customerRepository.Create(MapToDomain(customerDto));

            return MapToDto(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<List<long>> GetPurchasedToursByUser(long userId)
    {
        try
        {
            var customer = _customerRepository.GetByUser(userId);

            var tourPurchaseTokens = customer.TourPurchaseTokens;

            return tourPurchaseTokens?.Select(t => t.TourId).ToList() ?? new Result<List<long>>();
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

    public Result<bool> IsTourPurchasedByUser(long userId, long tourId)
    {
        try
        {
            var customer = _customerRepository.GetByUser(userId);
            
            return customer.OwnsTour(tourId);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}