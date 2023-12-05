using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public bool IsCreatedByUser(int userId)
        {
            return AuthorId == userId;
        }
    }
}
