using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterLocationUpdated : DomainEvent
    {
        public int TouristId { get; private set; }
        public DateTime DateOfUpdate { get; private set; }
        public double Longitude {  get; private set; }
        public double Latitude { get; private set; }
        public SocialEncounterLocationUpdated(long parentId, int touristId, DateTime dateOfUpdate, double longitude, double latitude) : base(parentId)
        {
            TouristId = touristId;
            DateOfUpdate = dateOfUpdate;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
