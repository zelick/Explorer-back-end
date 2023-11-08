using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public List<TourPurchaseTokenDto>? PurchaseTokens { get; set; }
        public long ShoppingCartId { get; set; }

    }
}
