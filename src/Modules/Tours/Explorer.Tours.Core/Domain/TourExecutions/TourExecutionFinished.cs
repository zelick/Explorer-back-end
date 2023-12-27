using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecutionFinished: DomainEvent
    {
        [JsonConstructor]
        public TourExecutionFinished(long aggregateId, DateTime finishDate) : base(aggregateId)
        {
            FinishDate = finishDate;
        }
        public TourExecutionFinished() { }
        public DateTime FinishDate { get; private set; }
    }
}
