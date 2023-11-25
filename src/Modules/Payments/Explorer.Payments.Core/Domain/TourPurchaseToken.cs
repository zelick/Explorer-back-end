using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Payments.Core.Domain;

public class TourPurchaseToken : ValueObject
{
    public long TourId { get; init; }
    public DateTime Timestamp { get; init; }

    [JsonConstructor]
    public TourPurchaseToken(long tourId)
    {
        TourId = tourId;
        Timestamp = DateTime.UtcNow;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TourId;
    }
}