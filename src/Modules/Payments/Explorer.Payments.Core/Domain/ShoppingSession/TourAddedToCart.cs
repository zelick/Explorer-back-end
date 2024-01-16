using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class TourAddedToCart : DomainEvent
{
    [JsonConstructor]
    public TourAddedToCart(long aggregateId, DateTime tourAdded, long tourId, int tourPrice, string tourName) : base(aggregateId)
    {
        TourId = tourId;
        TourPrice = tourPrice;
        TourName = tourName;
        TourAdded = tourAdded;
    }

    public TourAddedToCart() { }

    public long TourId { get; private set; }
    public int TourPrice { get; private set; }
    public string TourName { get; private set; }
    public DateTime TourAdded { get; private set; }
}