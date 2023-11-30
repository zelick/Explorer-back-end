using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class OrderItem : ValueObject
{
    public long ItemId { get; init; }
    public double Price { get; init; }

    [JsonConstructor]
    public OrderItem(long itemId, double price)
    {
        ItemId = itemId;
        Price = price;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ItemId;
        yield return Price;

    }
}

