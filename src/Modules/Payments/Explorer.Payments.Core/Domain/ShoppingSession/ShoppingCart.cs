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

    public bool IsEmpty()
    {
        return Items.Count == 0;
    }

    public void CheckOut()
    {
        if (Items.Count == 0) throw new InvalidOperationException("Can't check out because Shopping Cart is empty!");
        Items.Clear();
    }

    public void UpdateItem(OrderItem orderItem, Item item)
    {
        var index = Items.FindIndex(i => i.ItemId == orderItem.ItemId);

        if (index == -1) throw new ArgumentException("Order item not found in cart.");

        var updatedItem = new OrderItem(item.ItemId, item.Name, item.Price, item.Type);
        Items[index] = updatedItem;
    }
}