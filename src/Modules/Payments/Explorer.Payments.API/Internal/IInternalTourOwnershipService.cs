using FluentResults;

namespace Explorer.Payments.API.Internal;

public interface IInternalTourOwnershipService
{
    Result<bool> IsTourPurchasedByUser(long userId, long tourId);
    //ovde dodas metodu a implemenita se u ItemOwnershipServisu
}

