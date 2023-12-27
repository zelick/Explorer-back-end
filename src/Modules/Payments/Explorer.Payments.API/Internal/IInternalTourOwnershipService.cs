using FluentResults;

namespace Explorer.Payments.API.Internal;

public interface IInternalTourOwnershipService
{
    Result<bool> IsTourPurchasedByUser(long userId, long tourId);

    int GetPurchasesNumber(long tourId);
}

