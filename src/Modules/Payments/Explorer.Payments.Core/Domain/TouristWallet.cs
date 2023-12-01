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
        public int AdventureCoins { get; init; }
        public long UserId { get; init; }

        public TouristWallet(long userId)
        {
            UserId = userId;
            AdventureCoins = 0;
        }
    }
}
