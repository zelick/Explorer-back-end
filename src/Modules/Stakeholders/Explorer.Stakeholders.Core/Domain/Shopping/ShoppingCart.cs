using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Shopping
{
    public class ShoppingCart: Entity
    {
        public long TouristId { get; init; } //strani kljuc
        public List<OrderItem> Items { get; init; } = new List<OrderItem>();
        public double Price { get; set; } 

        public ShoppingCart(){}

        public ShoppingCart(long touristId)
        {
            this.TouristId = touristId;
            this.Items = new List<OrderItem>();
            this.Price = CalculateTotalPrice(); //provera?
        }

        public double CalculateTotalPrice()
        {

            var total = 0.0;
            foreach(var item in Items)
            {
                total += item.Price;
            }
            return total;
        }

        public void AddItemToShoppingCart(OrderItem item)
        {
            this.Items.Add(item);
        }
    }
}
