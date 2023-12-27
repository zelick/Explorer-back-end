using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecutionCheckpointCompleted: DomainEvent
    {
        public long CheckpointId {  get; set; }
        public DateTime Time {  get; set; }
        public int CheckpointIndex { get; set; }
        public TourExecutionCheckpointCompleted()
        {
                
        }

        [JsonConstructor]
        public TourExecutionCheckpointCompleted(long aggregateId, long checkpointId, DateTime time, int checkpointIndex): base(aggregateId)
        {
            CheckpointId = checkpointId;
            Time = time;
            CheckpointIndex = checkpointIndex;
        }
    }
}
