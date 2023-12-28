using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterLocationUpdated : DomainEvent
    {
        public DateTime DateOfUpdate { get; private set; }
        public int TouristId { get; private set; }
        public double Longitude {  get; private set; }
        public double Latitude { get; private set; }
        [JsonConstructor]
        public SocialEncounterLocationUpdated(long aggregateId, int touristId, DateTime dateOfUpdate, double longitude, double latitude) : base(aggregateId)
        {
            TouristId = touristId;
            DateOfUpdate = dateOfUpdate;
            Longitude = longitude;
            Latitude = latitude;
        }
        public SocialEncounterLocationUpdated() { }
    }
}
