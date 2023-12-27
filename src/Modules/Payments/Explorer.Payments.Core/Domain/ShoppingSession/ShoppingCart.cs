using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;
using Explorer.Payments.Core.Domain.Converters;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class ShoppingCart : EventSourcedAggregate
{
    public long UserId { get; init; }
    public List<OrderItem> Items;
    public DateTime LastActivityTime;

    [JsonConverter(typeof(ShoppingCartEventsConverter))]
    public override List<DomainEvent> Changes { get; set; }

    private const int MaxSessionMinutes = 30;

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
        if (item.Type == ItemType.Tour)
            Causes(new TourAddedToCart(this.Id, DateTime.UtcNow, item.ItemId, item.Price, item.Name));

        else if (item.Type == ItemType.Bundle)
            Causes(new BundleAddedToCart(this.Id, DateTime.UtcNow, item.ItemId, item.Price, item.Name));
    }

    public void RemoveItem(OrderItem item)
    {
        if (item.Type == ItemType.Tour)
            Causes(new TourRemovedFromCart(this.Id, DateTime.UtcNow, item.ItemId));

        else if (item.Type == ItemType.Bundle)
            Causes(new BundleRemovedFromCart(this.Id, DateTime.UtcNow, item.ItemId));
    }

    public void StartShoppingSession()
    {
        Causes(new ShoppingSessionStarted(this.Id, DateTime.UtcNow));
    }

    public void LeavePreviousSession()
    {
        Causes(new ShoppingSessionAbandoned(this.Id, DateTime.UtcNow));
    }

    public void UseCoupon(long couponId, long tourId)
    {
        Causes(new ShoppingCouponUsed(this.Id, DateTime.UtcNow, couponId, tourId));
    }

    public bool HasExpiredSession()
    {
        var sessionStartedEvent = Changes.OfType<ShoppingSessionStarted>().LastOrDefault();

        if (sessionStartedEvent == null) return false;

        var sessionDuration = DateTime.UtcNow - sessionStartedEvent.SessionStarted;
        return sessionDuration > TimeSpan.FromMinutes(MaxSessionMinutes);
    }

    public bool HasActiveSession()
    {
        var sessionStartedEvent = Changes.OfType<ShoppingSessionStarted>().LastOrDefault();

        if (sessionStartedEvent == null) return false;

        return !HasExpiredSession();
    }

    public bool IsEmpty()
    {
        return Items.Count == 0;
    }

    public void CheckOut()
    {
        if (Items.Count == 0) throw new InvalidOperationException("Can't check out because Shopping Cart is empty!");

        Causes(new ShoppingSessionEnded(this.Id, DateTime.UtcNow));
    }

    public void UpdateItem(OrderItem orderItem, Item item)
    {
        var index = Items.FindIndex(i => i.ItemId == orderItem.ItemId);

        if (index == -1) throw new ArgumentException("Order item not found in cart.");

        var updatedItem = new OrderItem(item.ItemId, item.Name, item.Price, item.Type);
        Items[index] = updatedItem;
    }

    private void CheckSessionActivity()
    {
        var maxInactivityDuration = TimeSpan.FromMinutes(MaxSessionMinutes);
        var elapsedInactivityTime = DateTime.UtcNow - LastActivityTime;

        if (elapsedInactivityTime > maxInactivityDuration)
            Causes(new ShoppingSessionAbandoned(Id, LastActivityTime));
    }

    private void Causes(DomainEvent @event)
    {
        Changes.Add(@event);
        Apply(@event);
    }

    public override void Apply(DomainEvent @event)
    {
        When((dynamic)@event);
        Version++;
    }

    private void When(ShoppingSessionStarted sessionStarted)
    {
        LastActivityTime = DateTime.UtcNow;
    }

    private void When(ShoppingSessionAbandoned sessionAbandoned)
    {
        LastActivityTime = DateTime.UtcNow;
    }

    private void When(ShoppingSessionEnded sessionEnded)
    {
        LastActivityTime = DateTime.UtcNow;

        Items.Clear();
    }

    private void When(ShoppingCouponUsed couponUsed)
    {
        LastActivityTime = DateTime.UtcNow;
        CheckSessionActivity();
    }

    private void When(TourAddedToCart tourAdded)
    {
        LastActivityTime = DateTime.UtcNow;
        CheckSessionActivity();

        Items.Add(new OrderItem(tourAdded.TourId, tourAdded.TourName, tourAdded.TourPrice, ItemType.Tour));
    }

    private void When(TourRemovedFromCart tourRemoved)
    {
        LastActivityTime = DateTime.UtcNow;
        CheckSessionActivity();

        var tourToRemove = Items.FirstOrDefault(item => item.ItemId == tourRemoved.TourId && item.Type == ItemType.Tour);

        if (tourToRemove != null)
        {
            Items.Remove(tourToRemove);
        }
    }

    private void When(BundleAddedToCart bundleAdded)
    {
        LastActivityTime = DateTime.UtcNow;
        CheckSessionActivity();

        Items.Add(new OrderItem(bundleAdded.BundleId, bundleAdded.BundleName, bundleAdded.BundlePrice, ItemType.Bundle));
    }

    private void When(BundleRemovedFromCart bundleRemoved)
    {
        LastActivityTime = DateTime.UtcNow;
        CheckSessionActivity();

        var bundleToRemove = Items.FirstOrDefault(item => item.ItemId == bundleRemoved.BundleId && item.Type == ItemType.Bundle);

        if (bundleToRemove != null)
        {
            Items.Remove(bundleToRemove);
        }
    }
}