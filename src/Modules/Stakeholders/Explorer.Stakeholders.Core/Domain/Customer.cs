using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Customer: Entity
    {
        public long TouristId { get; init; }
        public List<long>? PurchaseTokenIds { get; init; } = new();
        public long ShoppingCartId { get; init; } 

        public Customer() { }

        public Customer(long touristId, long shoppingCartId)
        {
            TouristId = touristId;
            ShoppingCartId = shoppingCartId;
        }

        public void CustomersPurchaseTokens (long purchaseTokenId)
        {
            PurchaseTokenIds?.Add(purchaseTokenId);
        }
    }
}