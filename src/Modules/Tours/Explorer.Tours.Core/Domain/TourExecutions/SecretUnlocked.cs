using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class SecretUnlocked: DomainEvent
    {
        public DateTime DateTime { get; set; }
        public long CheckpointId { get; set; }

        public SecretUnlocked()
        {
            
        }
        [JsonConstructor]
        public SecretUnlocked(long aggregateId, DateTime dateTime, long checkpointId) : base(aggregateId)
        {
            DateTime = dateTime;
            CheckpointId = checkpointId;
        }
    }
}
