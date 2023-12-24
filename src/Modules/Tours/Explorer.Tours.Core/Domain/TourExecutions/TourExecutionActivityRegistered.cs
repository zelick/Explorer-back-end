using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecutionActivityRegistered: DomainEvent
    {
        public TourExecutionActivityRegistered(long aggregateId, DateTime startdate, double longitude, double latitude) : base(aggregateId)
        {
            StartDate = startdate;
            Longitude = longitude;
            Latitude = latitude;
        }
        public DateTime StartDate { get; private set; }
        public double Longitude { get; private set;}
        public double Latitude { get; private set;}
    }
}
