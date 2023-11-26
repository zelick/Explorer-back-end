using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class ShoppingCart : Entity
{
    public long UserId { get; init; }
    public List<OrderItem>? Items { get; init; }
    public double Price { get; private set; }

    public ShoppingCart(long userId, List<OrderItem>? items = null)
    {
        UserId = userId;
        Items = items;
        CalculateTotalPrice();
    }

    public void CalculateTotalPrice()
    {
        Price = Items?.Sum(item => item.Price) ?? 0;
    }

    public List<long> CheckOut()
    {
        if(Items is null) throw new InvalidOperationException("Can't check out because Shopping Cart is empty!");

        var purchasedItemsId = Items.Select(i => i.ItemId).ToList();

        Items.Clear();
        Price = 0;

        return purchasedItemsId;
    }
    public bool IsOwnedByUser(int userId)
    {
        return UserId == userId;
    }
}