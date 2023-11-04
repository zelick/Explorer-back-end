using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Shopping
{
    public class ShoppingCartDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public double Price { get; set; }
    }
}
