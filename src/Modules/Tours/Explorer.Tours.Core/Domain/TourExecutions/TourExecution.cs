using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using System.Text.Json.Serialization;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecution : EventSourcedAggregate
    {
        public long TouristId { get; init; }
        public long TourId { get; init; }
        public Tour? Tour { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime LastActivity { get; private set; }
        public ExecutionStatus ExecutionStatus { get; private set; }
        public List<CheckpointCompletition> CompletedCheckpoints { get; private set; }

        [JsonConverter(typeof(TourExecutionEventsConverter))]
        public override List<DomainEvent> Changes { get; set; }

        public TourExecution(long touristId, long tourId)
        {
            TouristId = touristId;
            TourId = tourId;
            Causes(new TourExecutionStarted(this.Id, DateTime.UtcNow));
        }

        public TourExecution RegisterActivity(float longitude, float latitude)
        {
            Causes(new TourExecutionActivityRegistered(this.Id, DateTime.UtcNow, longitude, latitude));
            return this;
        }
        
        public void CheckTourCompletition()
        {
            if (CompletedCheckpoints.Count == Tour.Checkpoints.Count)
            {
                ExecutionStatus = ExecutionStatus.Completed;
                Causes(new TourExecutionFinished(this.Id, DateTime.UtcNow));
            }
        }

        public void Abandone(long Id)
        {
            if (Id == this.Id)
                ExecutionStatus = ExecutionStatus.Abandoned;
            Causes(new TourExecutionFinished(this.Id, DateTime.UtcNow));
        }

        public double CalculateTourProgressPercentage()
        {
            double percentage = 0;
            int checkpointsCount = Tour.Checkpoints.Count();
            int completedCheckpointsCount = CompletedCheckpoints.Count();

            if (checkpointsCount > 0)
            {
                percentage = (double)completedCheckpointsCount / checkpointsCount * 100;
            }

            return percentage;
        }

        public void setTour(Tour tour)
        {
            Tour = tour;
        }

        public bool IsTourProgressAbove35Percent()
        {
            double progressPercentage = CalculateTourProgressPercentage();

            return progressPercentage > 35.0;
        }

        public bool HasOneWeekPassedSinceLastActivity()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan difference = currentTime - LastActivity;

            return difference.TotalDays > 7;
        }
        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        public override void Apply(DomainEvent @event)
        {
            LastActivity = DateTime.UtcNow;
            When((dynamic)@event);
            Version++;
        }

        private void When(TourExecutionFinished finished)
        {
            
        }

        private void When(TourExecutionActivityRegistered activityRegistered)
        {
            foreach (Checkpoint checkpoint in Tour.Checkpoints)
            {
                double a = Math.Abs(Math.Round(checkpoint.Longitude, 4) - Math.Round(activityRegistered.Longitude, 4));
                double b = Math.Abs(Math.Round(checkpoint.Latitude, 4) - Math.Round(activityRegistered.Latitude, 4));
                if (a < 0.01 && b < 0.01)
                {
                    CheckpointCompletition checkpointCompletition = new CheckpointCompletition(checkpoint.Id);
                    if (CompletedCheckpoints.Find(c => c.TourExecutionId == Id && c.CheckpointId == checkpoint.Id) == null)
                        CompletedCheckpoints.Add(checkpointCompletition);
                }
                CheckTourCompletition();
            }
        }

        private void When(TourExecutionStarted started)
        {
            Start = started.StartDate;
            ExecutionStatus = ExecutionStatus.InProgress;
            CompletedCheckpoints = new List<CheckpointCompletition>();
        }
    }

    public enum ExecutionStatus
    {
        Completed,
        Abandoned,
        InProgress
    }
}
