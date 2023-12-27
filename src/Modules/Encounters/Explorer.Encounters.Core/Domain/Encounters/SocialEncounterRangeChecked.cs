using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterRangeChecked : DomainEvent
    {
        public DateTime RangeCheckedDate { get; private set; }
        public List<int> ActiveTouristsIds { get; private set; }
        [JsonConstructor]
        public SocialEncounterRangeChecked(long aggregateId, List<int> activeTouristsIds, DateTime rangeCheckedDate) : base(aggregateId)
        {
            ActiveTouristsIds = activeTouristsIds;
            RangeCheckedDate = rangeCheckedDate;
        }
        public SocialEncounterRangeChecked() { }
    }
}
