using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Payments.Core.Domain.ShoppingSession;

public class ShoppingCouponUsed : DomainEvent
{
    [JsonConstructor]
    public ShoppingCouponUsed(long aggregateId, DateTime couponUsed, long couponId, long discountedTourId) : base(aggregateId)
    {
        CouponId = couponId;
        DiscountedTourId = discountedTourId;
        CouponUsed = couponUsed;
    }

    public ShoppingCouponUsed() { }

    public long CouponId { get; private set; }
    public long DiscountedTourId { get; private set; }
    public DateTime CouponUsed { get; private set; }
}