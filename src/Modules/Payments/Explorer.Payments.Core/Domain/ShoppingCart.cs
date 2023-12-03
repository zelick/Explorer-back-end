using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class ShoppingCart : Entity
{
    public long UserId { get; init; }
    public List<OrderItem> Items;

    public ShoppingCart(long userId)
    {
        UserId = userId;
        Items = new List<OrderItem>();
    }

    public int GetTotal()
    {
        return Items.Sum(item => item.Price);
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public void RemoveItem(OrderItem item)
    {
        Items.Remove(item);
    }

    public List<long> CheckOut()
    {
        if (Items.Count == 0) throw new InvalidOperationException("Can't check out because Shopping Cart is empty!");

        var purchasedItemsId = Items.Select(i => i.ItemId).ToList();

        Items.Clear();

        return purchasedItemsId;
    }
}