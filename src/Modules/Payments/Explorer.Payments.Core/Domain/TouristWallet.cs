using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class TouristWallet : Entity
{
    public int AdventureCoins { get; private set; }
    public long UserId { get; init; }

    public TouristWallet(long userId)
    {
        UserId = userId;
        AdventureCoins = 0;
    }

    public void PaymentAdventureCoins(int coins)
    {
        if (coins < 0) throw new ArgumentException("AdventureCoins must be a positive value.", nameof(coins));

        AdventureCoins += coins;
    }
    
    public void Pay(int amount)
    {
        if (amount < 0) throw new ArgumentException("Amount to pay must be a positive value.", nameof(amount));
        if (AdventureCoins < amount) throw new InvalidOperationException("Not enough AdventureCoins to complete the payment.");

        AdventureCoins -= amount;
    }
}