using FluentResults;

namespace Explorer.Payments.API.Internal;

public interface IInternalShoppingService
{
    Result<bool> IsTourPurchasedByUser(long userId, long tourId);
}

