using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class Item : Entity
{
    public long SellerId { get; init; }
    public long ItemId { get; init; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public ItemType Type { get; init; }

    public Item(long sellerId, long itemId, string name, int price, ItemType type)
    {
        SellerId = sellerId;
        ItemId = itemId;
        Name = name;
        Price = price;
        Type = type;
        Validate();
    }

    public void UpdatePrice(int price)
    {
        Price = price;
        Validate();
    }

    public void Update(string name, int price)
    {
        Name = name;
        Price = price;
        Validate();
    }

    private void Validate()
    {
        if (SellerId == 0) throw new ArgumentException("Invalid SellerId");
        if (ItemId == 0) throw new ArgumentException("Invalid ItemId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (Price < 0) throw new ArgumentException("Invalid Price.");
    }
}

public enum ItemType
{
    Tour,
    Bundle
}