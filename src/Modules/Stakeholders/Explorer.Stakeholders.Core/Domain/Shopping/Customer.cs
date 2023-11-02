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
       // public long ShoppingCartId { get; init; }
       //lista tokena

        //metoda prosledim token kao param 
        //dodaje u listu 
        //iz servisa se ne poziva rep nego meotda iz modela 
        //iz neke put metode iz kontorl servis se pozova 
        public Customer()
        {

        }
        public Customer(long toruistId)
        {
            this.TouristId = toruistId; 
        }
    }
}
