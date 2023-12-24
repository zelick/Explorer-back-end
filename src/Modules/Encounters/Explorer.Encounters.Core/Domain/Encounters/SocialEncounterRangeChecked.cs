using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterRangeChecked : DomainEvent
    {
        public List<int> ActiveTouristsIds { get; private set; }
        public DateTime RangeCheckedDate { get; private set; }
        public SocialEncounterRangeChecked(long parentId, List<int> activeTouristsIds, DateTime rangeCheckedDate) : base(parentId)
        {
            ActiveTouristsIds = activeTouristsIds;
            RangeCheckedDate = rangeCheckedDate;
        }
    }
}
