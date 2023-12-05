namespace Explorer.Payments.Core.Domain;

public class BundleItem : Item
{ 
    public List<long> BundleItemIds { get; init; }

    public BundleItem(long authorId, long itemId, string name, int price) : base(authorId, itemId, name, price, ItemType.Bundle)
    {
        BundleItemIds = new List<long>();
    }
}