using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecution : Entity
    {
        public long TouristId { get; init; }
        public long TourId { get; init; }
        public Tour? Tour { get; private set; }
        public DateTime Start { get; init; }
        public DateTime LastActivity { get; private set; }
        public ExecutionStatus ExecutionStatus { get; private set; }
        public List<CheckpointCompletition> CompletedCheckpoints { get; init; }

        public TourExecution(long touristId, long tourId)
        {
            TouristId = touristId;
            TourId = tourId;
            Start = DateTime.UtcNow;
            LastActivity = DateTime.UtcNow;
            ExecutionStatus = ExecutionStatus.InProgress;
            CompletedCheckpoints = new List<CheckpointCompletition>();
        }

        public TourExecution RegisterActivity(float longitude, float latitude)
        {
            foreach (Checkpoint checkpoint in Tour.Checkpoints)
            {
                double a = Math.Abs(Math.Round(checkpoint.Longitude, 4) - Math.Round(longitude, 4));
                double b = Math.Abs(Math.Round(checkpoint.Latitude, 4) - Math.Round(latitude, 4));
                if (a < 0.01 && b < 0.01)
                {
                    CheckpointCompletition checkpointCompletition = new CheckpointCompletition(checkpoint.Id);
                    if (!CompletedCheckpoints.Contains(checkpointCompletition))
                        CompletedCheckpoints.Add(checkpointCompletition);
                }


                LastActivity = DateTime.UtcNow;
                CheckTourCompletition();
            }
            return this;
        }

        public void CheckTourCompletition()
        {
            if (CompletedCheckpoints.Count == Tour.Checkpoints.Count)
                ExecutionStatus = ExecutionStatus.Completed;
        }

        public void Abandone(long Id)
        {
            if (Id == this.Id)
                ExecutionStatus = ExecutionStatus.Abandoned;
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
    }

    public enum ExecutionStatus
    {
        Completed,
        Abandoned,
        InProgress
    }
}
