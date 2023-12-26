using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class SecureToken : Entity
    {
        public long UserId { get; private set; }
        public string TokenData { get; private set; }
        public DateTime ExpiryTime { get; private set; }

        public SecureToken(long userId, string tokenData, DateTime expiryData)
        {
            this.UserId = userId;
            TokenData = tokenData;
            ExpiryTime = expiryData;
        }
    }
}
