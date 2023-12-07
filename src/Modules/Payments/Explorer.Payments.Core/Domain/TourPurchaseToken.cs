using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class TourPurchaseToken : Entity
{
    public long UserId { get; init; }
    public long TourId { get; init; }

    public TourPurchaseToken(long userId, long tourId)
    {
        UserId = userId;
        TourId = tourId;
    }
}