using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecutionStarted: DomainEvent
    {
        [JsonConstructor]
        public TourExecutionStarted(long aggregateId, DateTime startdate):base(aggregateId)
        {
            StartDate = startdate;
        }
        public TourExecutionStarted()
        {

        }
        public DateTime StartDate { get; private set; }
    }
}
