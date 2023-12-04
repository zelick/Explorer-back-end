using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class AdventureCoin : ValueObject
{
    public int Amount { get; init; } 

    public AdventureCoin() { }

    [JsonConstructor]
    public AdventureCoin(int amount)
    {
        if(amount < 0) throw new ArgumentException("AC amount cannot be a negative number");
        Amount = amount;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}