using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class TourPurchaseToken : Entity
{
    public long UserId { get; init; }
    public long TourId { get; init; }
    public DateTime Timestamp { get; init; }
    // TODO does it need timestamp?

    public TourPurchaseToken(long userId, long tourId)
    {
        UserId = userId;
        TourId = tourId;
        Timestamp = DateTime.UtcNow;
    }
}