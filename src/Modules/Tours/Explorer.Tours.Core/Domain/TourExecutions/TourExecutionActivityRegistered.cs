using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecutionActivityRegistered: DomainEvent
    {
        [JsonConstructor]
        public TourExecutionActivityRegistered(long aggregateId, DateTime date, double longitude, double latitude) : base(aggregateId)
        {
            Date = date;
            Longitude = longitude;
            Latitude = latitude;
        }
        public TourExecutionActivityRegistered() { }
        public DateTime Date { get; private set; }
        public double Longitude { get; private set;}
        public double Latitude { get; private set;}
    }
}
