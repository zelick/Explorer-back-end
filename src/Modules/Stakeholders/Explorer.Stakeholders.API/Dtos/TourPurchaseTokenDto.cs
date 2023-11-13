using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TourPurchaseTokenDto
    {
        public long CustomerId { get; set; }
        public long TourId { get; set; }
    }
}