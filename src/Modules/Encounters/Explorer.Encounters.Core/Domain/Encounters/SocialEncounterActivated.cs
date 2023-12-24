using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterActivated : DomainEvent
    {
        public int TouristId { get; private set; }
        public DateTime ActivationDate { get; private set; }
        public SocialEncounterActivated(long parentId,int touristId, DateTime activationDate) : base(parentId)
        {
            TouristId = touristId;
            ActivationDate = activationDate;
        }
    }
}
