using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterCompleted : DomainEvent
    {
        public DateTime CompletionDate { get; private set; }
        public int TouristId { get; private set; }
        public SocialEncounterCompleted(long parentId, DateTime completionDate, int touristId) : base(parentId)
        {
            CompletionDate = completionDate;
            TouristId = touristId;
        }
    }
}
