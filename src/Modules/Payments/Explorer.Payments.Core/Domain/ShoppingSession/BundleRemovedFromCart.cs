using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class BundleRemovedFromCart : DomainEvent
{
    [JsonConstructor]
    public BundleRemovedFromCart(long aggregateId, DateTime bundleRemoved, long bundleId) : base(aggregateId)
    {
        BundleId = bundleId;
        BundleRemoved = bundleRemoved;
    }

    public BundleRemovedFromCart() { }

    public long BundleId { get; private set; }
    public DateTime BundleRemoved { get; private set; }
}