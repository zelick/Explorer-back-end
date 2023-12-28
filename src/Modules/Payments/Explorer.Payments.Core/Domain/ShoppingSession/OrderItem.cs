using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class OrderItem : ValueObject
{
    public long ItemId { get; init; }
    public string Name { get; init; }
    public int Price { get; init; }
    public ItemType Type { get; init; }

    public OrderItem() { }

    [JsonConstructor]
    public OrderItem(long itemId, string name, int price, ItemType type)
    {
        ItemId = itemId;
        Name = name;
        Price = price;
        Type = type;
        Validate();
    }

    private void Validate()
    {
        if (ItemId == 0) throw new ArgumentException("Invalid ItemId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (Price < 0) throw new ArgumentException("Invalid Price.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ItemId;
        yield return Name;
        yield return Price;
        yield return Type;
    }
}