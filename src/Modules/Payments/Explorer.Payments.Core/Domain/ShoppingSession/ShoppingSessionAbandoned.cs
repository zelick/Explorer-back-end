using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class ShoppingSessionAbandoned : DomainEvent
{
    [JsonConstructor]
    public ShoppingSessionAbandoned(long aggregateId, DateTime sessionAbandoned) : base(aggregateId)
    {
        SessionAbandoned = sessionAbandoned;
    }

    public ShoppingSessionAbandoned() { }

    public DateTime SessionAbandoned { get; private set; }
}