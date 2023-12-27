using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class ShoppingSessionEnded : DomainEvent
{
    [JsonConstructor]
    public ShoppingSessionEnded(long aggregateId, DateTime sessionEnded) : base(aggregateId)
    {
        SessionEnded = sessionEnded;
    }

    public ShoppingSessionEnded() { }

    public DateTime SessionEnded { get; private set; }
}