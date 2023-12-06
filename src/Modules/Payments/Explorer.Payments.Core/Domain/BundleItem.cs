namespace Explorer.Payments.Core.Domain;

public class BundleItem : Item
{ 
    public List<long> BundleItemIds { get; init; }

    public BundleItem(long sellerId, long itemId, string name, int price) : base(sellerId, itemId, name, price, ItemType.Bundle)
    {
        BundleItemIds = new List<long>();
    }

    public BundleItem(BundleItem item) : base(item)
    {
        BundleItemIds = item.BundleItemIds;
    }
}