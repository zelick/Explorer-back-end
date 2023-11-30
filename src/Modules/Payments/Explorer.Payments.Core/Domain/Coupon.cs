using System.Runtime.InteropServices.JavaScript;
using Explorer.BuildingBlocks.Core.Domain;
using System.Text;

namespace Explorer.Payments.Core.Domain
{
    public class Coupon : Entity
    {
        public string Code { get; init; }
        public int DiscountPercentage { get; init; }
        public DateTime? ExpirationDate { get; init; }
        public bool IsGlobal { get; init; }
        public long? TourId { get; init; }
        


        public Coupon() { }

        public Coupon(int discountPercentage, DateTime? expirationDate, long? tourId, bool isGlobal)
        {
            Code = GenerateCode();
            DiscountPercentage = discountPercentage;
            ExpirationDate = expirationDate;
            TourId = tourId;
            IsGlobal = isGlobal;

            Validate();
        }

        private void Validate()
        {
            if (DiscountPercentage < 0) throw new ArgumentException("Discount cannot be a negative number");
            if (ExpirationDate <= DateTime.Now) throw new ArgumentException("The expiration date must not be in the past.");
            if (TourId == 0) throw new ArgumentException("Invalid TourId.");
            if (!IsGlobalVoucherWithNullTourId()) throw new ArgumentException("If the voucher is global, the TourId must be null.");
        }

        private bool IsGlobalVoucherWithNullTourId()
        {
            return IsGlobal && TourId == null;
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
    }
}
