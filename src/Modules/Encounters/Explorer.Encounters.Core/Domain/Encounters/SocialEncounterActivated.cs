using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain.Converters;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterActivated : DomainEvent
    {
        public DateTime ActivationDate { get; private set; }
        public long TouristId { get; private set; }
        [JsonConstructor]
        public SocialEncounterActivated(long aggregateId, long touristId, DateTime activationDate) : base(aggregateId)
        {
            TouristId = touristId;
            ActivationDate = activationDate;
        }
        public SocialEncounterActivated() { }
    }
}
