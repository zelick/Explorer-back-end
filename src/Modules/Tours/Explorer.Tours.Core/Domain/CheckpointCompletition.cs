using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class CheckpointCompletition: Entity
    {
        public long TourExecutionId { get; init; }
        public long CheckpointId { get; init; }
        public DateTime CompletitionTime { get; init; }

        public CheckpointCompletition(long checkpointId)
        {
            CheckpointId = checkpointId;
            CompletitionTime = DateTime.Now;
        }

    }
}
