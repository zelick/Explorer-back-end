using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

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
