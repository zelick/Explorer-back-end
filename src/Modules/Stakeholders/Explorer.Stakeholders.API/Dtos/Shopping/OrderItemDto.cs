using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Shopping
{
    public class OrderItemDto
    {
        public long TourId { get; set; }
        public double Price { get; set; }
    }
}
