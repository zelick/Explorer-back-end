using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class OrderItem : ValueObject
{
    public string ItemName { get; init; }
    public long ItemId { get; init; }
    public double Price { get; init; }
    public OrderItemType Type { get; init; }

    public OrderItem() { }

    [JsonConstructor]
    public OrderItem(string itemName, long itemId, double price, OrderItemType type)
    {
        ItemName = itemName;
        ItemId = itemId;
        Price = price;
        Type = type;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ItemName;
        yield return ItemId;
        yield return Price;
        yield return Type;
    }
}

public enum OrderItemType
{
    Tour,
    Bundle
}
