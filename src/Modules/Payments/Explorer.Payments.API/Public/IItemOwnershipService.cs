using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IItemOwnershipService
{
    Result<List<long>> GetPurchasedToursByUser(long userId);
    Result<bool> IsTourPurchasedByUser(long userId, long tourId);
}

