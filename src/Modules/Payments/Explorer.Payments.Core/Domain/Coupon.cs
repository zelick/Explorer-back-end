using Explorer.BuildingBlocks.Core.Domain;
using System.Text;

namespace Explorer.Payments.Core.Domain
{
    public class Coupon : Entity
    {
        public long SellerId { get; init; }
        public string Code { get; init; }
        public int DiscountPercentage { get; init; }
        public DateTime? ExpirationDate { get; init; }
        public bool IsGlobal { get; init; }
        public long? TourId { get; init; }
        public bool IsUsed { get; private set; }

        public Coupon() { }

        public Coupon(long sellerId, int discountPercentage, DateTime? expirationDate, long? tourId, bool isGlobal)
        {
            SellerId = sellerId;
            Code = GenerateCode();
            DiscountPercentage = discountPercentage;
            ExpirationDate = expirationDate;
            TourId = tourId;
            IsGlobal = isGlobal;
            IsUsed = false;

            Validate();
        }

        private void Validate()
        {
            if (DiscountPercentage < 0) throw new ArgumentException("Discount cannot be a negative number");
            if (ExpirationDate <= DateTime.UtcNow) throw new ArgumentException("The expiration date must not be in the past.");
            if (TourId == 0) throw new ArgumentException("Invalid TourId.");
            if (IsGlobalVoucherWithNullTourId()) throw new ArgumentException("If the voucher is global, the TourId must be null.");
        }

        private bool IsGlobalVoucherWithNullTourId()
        {
            return !IsGlobal && TourId == null;
        }

        private string GenerateCode()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int codeLength = 8;

            Random random = new Random();
            StringBuilder codeBuilder = new StringBuilder(codeLength);

            for (int i = 0; i < codeLength; i++)
            {
                char randomChar = allowedChars[random.Next(allowedChars.Length)];
                codeBuilder.Append(randomChar);
            }

            return codeBuilder.ToString();
        }

        public long Apply(List<Item> tours)
        {
            if (!IsValid()) throw new InvalidOperationException($"Coupon can't be applied, expired on {ExpirationDate} or used.");

            Item discountedTour;
            if (IsGlobal)
            {
                discountedTour = tours.Where(tour => tour.SellerId == SellerId).MaxBy(tour => tour.Price)
                                 ?? throw new InvalidOperationException($"Coupon can only be applied on tours made by author {SellerId}.");
            }
            else
            {
                discountedTour = tours.FirstOrDefault(tour => tour.ItemId == TourId)
                                 ?? throw new InvalidOperationException($"Coupon can only be applied on tour {TourId}.");
            }

            ApplyDiscount(discountedTour);
            IsUsed = true;

            return discountedTour.ItemId;
        }

        private void ApplyDiscount(Item item)
        {
            var discountedPrice = item.Price - (item.Price * DiscountPercentage / 100);
            item.UpdatePrice(discountedPrice);
        }
        
        public bool IsValid()
        {
            return (!ExpirationDate.HasValue || ExpirationDate.Value >= DateTime.UtcNow) && !IsUsed;
        }
    }
}
