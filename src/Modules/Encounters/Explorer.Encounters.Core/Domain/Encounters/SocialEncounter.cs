using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounter : ValueObject
    {
        public int RequiredPeople { get; init; }
        public double Range { get; init; }  
        public List<int>? ActiveTouristsIds { get; init; }
        public SocialEncounter() { }

        [JsonConstructor]
        public SocialEncounter(int requiredPeople,double range)
        {
            RequiredPeople = requiredPeople;
            Range = range;
            ActiveTouristsIds = new List<int>();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return RequiredPeople;
            yield return Range;
            yield return ActiveTouristsIds;
        }




    }
}
