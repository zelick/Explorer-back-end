using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class VerificationToken : Entity
    {
        public long UserId {  get; private set; }
        public DateTime TokenCreationTime { get; private set; }
        public string TokenData { get; private set; }

        public VerificationToken(long userId, DateTime tokenCreationTime, string tokenData)
        {
            this.UserId = userId;
            this.TokenCreationTime = tokenCreationTime;
            TokenData = tokenData;
        }

    }
}
