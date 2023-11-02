using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Shopping
{
    public class Customer: Entity
    {
        public long TouristId { get; init; }
        public List<TourPurchaseToken>? PurchaseTokens { get; init; } = new List<TourPurchaseToken>();
        // public long ShoppingCartId { get; init; } //strani kljuc !!

        public Customer() { }
        public Customer(long toruistId)
        {
            TouristId = toruistId;
            PurchaseTokens = new List<TourPurchaseToken>();
        }

        //ovo zovem iz nekog servisa 
        public void CustomersPurchaseTokens (TourPurchaseToken purchaseToken)
        {
            PurchaseTokens.Add(purchaseToken);
        }
    }
}