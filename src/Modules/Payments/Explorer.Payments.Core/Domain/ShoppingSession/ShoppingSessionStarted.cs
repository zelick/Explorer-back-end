using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class ShoppingSessionStarted : DomainEvent
{
    [JsonConstructor]
    public ShoppingSessionStarted(long aggregateId, DateTime sessionStarted) : base(aggregateId)
    {
        SessionStarted = sessionStarted;
    }

    public ShoppingSessionStarted() { }

    public DateTime SessionStarted { get; private set; }
}