using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class TouristWallet : Entity
    {
        public int AdventureCoins { get; private set; }
        public long UserId { get; init; }

        public TouristWallet(long userId)
        {
            UserId = userId;
            AdventureCoins = 0;
        }

        public void PaymentAdventureCoins(int coins)
        {
            AdventureCoins += coins;
        }
    }
}
