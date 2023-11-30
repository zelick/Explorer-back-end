using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ICustomerService
{
    Result<CustomerDto> GetByUser(long userId);
    Result<CustomerDto> Create(CustomerDto customer);
    Result<List<long>> GetPurchasedToursByUser(long userId);
    Result<bool> IsTourPurchasedByUser(long userId, long tourId);
}

