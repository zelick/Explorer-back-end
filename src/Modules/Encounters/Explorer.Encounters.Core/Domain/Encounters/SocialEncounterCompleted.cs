using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterCompleted : DomainEvent
    {
        public DateTime CompletionDate { get; private set; }
        public long TouristId { get; private set; }
        [JsonConstructor]
        public SocialEncounterCompleted(long aggregateId, DateTime completionDate, long touristId) : base(aggregateId)
        {
            CompletionDate = completionDate;
            TouristId = touristId;
        }
        public SocialEncounterCompleted() { }
    }
}
