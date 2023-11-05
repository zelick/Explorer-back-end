using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Shopping
{
    public class OrderItem: ValueObject
    {
        public long TourId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public OrderItem() { }

        [JsonConstructor]
        public OrderItem(long tourId, double price, int quantity)
        {
            TourId = tourId;
            Price = price;
            Quantity = quantity;

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TourId;
            yield return Price; 
            yield return Quantity;
        }
    }
}
