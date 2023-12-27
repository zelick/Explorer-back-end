using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class BundleAddedToCart : DomainEvent
{
    [JsonConstructor]
    public BundleAddedToCart(long aggregateId, DateTime tourAdded, long bundleId, int bundlePrice, string bundleName) : base(aggregateId)
    {
        BundleId = bundleId;
        BundlePrice = bundlePrice;
        BundleName = bundleName;
        TourAdded = tourAdded;
    }

    public BundleAddedToCart() { }

    public long BundleId { get; private set; }
    public int BundlePrice { get; private set; }
    public string BundleName { get; private set; }
    public DateTime TourAdded { get; private set; }
}