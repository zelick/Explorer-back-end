using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class TourRemovedFromCart : DomainEvent
{
    [JsonConstructor]
    public TourRemovedFromCart(long aggregateId, DateTime tourRemoved, long tourId) : base(aggregateId)
    {
        TourId = tourId;
        TourRemoved = tourRemoved;
    }

    public TourRemovedFromCart() { }

    public long TourId { get; private set; }
    public DateTime TourRemoved { get; private set; }
}