using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class PaymentRecord : Entity
{
    public long UserId { get; init; }
    public long ItemId { get; set; }
    public int Price { get; set; }
    public DateTime Timestamp { get; init; }

    public PaymentRecord(long userId, long itemId, int price)
    {
        UserId = userId;
        ItemId = itemId;
        Price = price;
        Timestamp = DateTime.UtcNow;
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (ItemId == 0) throw new ArgumentException("Invalid ItemId");
        if (Price < 0) throw new ArgumentException("Invalid Price.");
    }
}
