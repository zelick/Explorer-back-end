using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class CheckoutCompletition: Entity
    {
        public long CheckpointId { get; init; }
        public DateTime CompletitionTime { get; init; }

        public CheckoutCompletition(long checkpointId)
        {
            CheckpointId = checkpointId;
            CompletitionTime = DateTime.Now;
        }

    }
}
