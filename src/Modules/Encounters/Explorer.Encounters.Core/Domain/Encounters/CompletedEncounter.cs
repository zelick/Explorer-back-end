using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class CompletedEncounter:ValueObject
    {
        public long TouristId { get; init; }
        public DateTime Completition {  get; init; }

        [JsonConstructor]
        public CompletedEncounter(long touristId,DateTime completition)
        {
           TouristId = touristId;
            Completition = completition;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TouristId;
            yield return Completition;

        }
    }
}
