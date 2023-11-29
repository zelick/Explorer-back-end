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
        public List<int> ToursIds { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Discount { get; set; }

        public Sale() { }

        public Sale(List<int> toursIds, DateTime start, DateTime end, int discount)
        {
            ToursIds = toursIds;
            Start = start;
            End = end;
            Discount = discount;
        }
    }
}
