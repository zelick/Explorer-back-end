using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Explorer.Payments.Core.Domain
{
    public class Sale: Entity
    {
        public List<long> ToursIds { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Discount { get; set; }
        public long AuthorId { get; set; }

        public Sale() { }

        public Sale(List<long> toursIds, DateTime start, DateTime end, int discount, long authorId)
        {
            ToursIds = toursIds;
            Start = start;
            End = end;
            Discount = discount;
            AuthorId = authorId;
            Validate();
        }

        public bool IsActive(DateTime currentDate)
        {
            return currentDate >= Start && currentDate <= End;
        }

        public int ApplyDiscount(int originalPrice)
        {
            return originalPrice - (originalPrice * Discount / 100);
        }

        private void Validate()
        {
            if (ToursIds == null || ToursIds.Count == 0) throw new ArgumentException("At least one tour is required for a sale.");
            if (Start >= End) throw new ArgumentException("Start date must be before the end date.");
            if (Discount <= 0) throw new ArgumentException("Discount must be greater than 0.");
        }

        public bool IsCreatedByUser(int userId)
        {
            return AuthorId == userId;
        }
    }
}
