namespace Explorer.Payments.API.Dtos
{
    public class CreateCouponDto
    {
        public long SellerId { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsGlobal { get; set; }
        public long? TourId { get; set; }
    }
}
