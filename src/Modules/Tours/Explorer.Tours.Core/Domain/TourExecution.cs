using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.Domain
{
    public class TourExecution:Entity
    {
        public long TouristId { get; init; }
        public long TourId { get; init; }
        public Tour Tour { get; init; }
        public DateTime Start {  get; init; }
        public  DateTime LastActivity { get; private set; }
        public ExecutionStatus ExecutionStatus { get; private set; }

        public List<CheckpointCompletition> CompletedCheckpoints { get; init; }

        public TourExecution(long touristId,long tourId) 
        {
            if(touristId < 0)
            {
                throw new ArgumentException("Invalid tourist.");
            }
            if (tourId < 0)
            {
                throw new ArgumentException("Invalid tour.");
            }

            TouristId = touristId;
            TourId = tourId;
            Start= DateTime.Now;
            LastActivity = DateTime.Now;
            ExecutionStatus = ExecutionStatus.InProgress;
            CompletedCheckpoints = new List<CheckpointCompletition>();
        }

        public TourExecution RegisterActivity(float longitude, float latitude)
        {
        /*   foreach(Checkpoint checkpoint in this.Tour.Checkpoints)
            {
                if((checkpoint.Longitude>=longitude+0.05 || checkpoint.Longitude<= longitude - 0.05) &&
                    (checkpoint.Latitude>= latitude + 0.05 || checkpoint.Latitude <= latitude - 0.05))
                {
                    CheckpointCompletition checkpointCompletition = new CheckpointCompletition(checkpoint.Id);
                    if(!this.CompletedCheckpoints.Contains(checkpointCompletition))
                        this.CompletedCheckpoints.Add(checkpointCompletition);
                }


                this.LastActivity=DateTime.Now;
                CheckTourCompletition();
            }
        nije dobra veza sa turom ne napuni turu
         
         */


           return this;

        }


        public void CheckTourCompletition()
        {
            if (this.CompletedCheckpoints.Count == this.Tour.Checkpoints.Count)
                this.ExecutionStatus= ExecutionStatus.Completed;
        }

        public void Abandone(long Id)
        {
            if (Id == this.Id)
                this.ExecutionStatus = ExecutionStatus.Abandoned;
        }

        public double CalculateTourProgressPercentage()
        {
            double percentage = 0;

            //OTKOMENTARISATI KAD POVEZEMO RESENJA - za sad ne hvata Tour
            //int checkpointsCount = Tour.Checkpoints.Count();
            //int completedCheckpointsCount = this.CompletedCheckpoints.Count();
            //int checkpointsCount = 5;
            //int completedCheckpointsCount = 1;
            int checkpointsCount = 5;
            int completedCheckpointsCount = 4;

            if (checkpointsCount > 0)
            {
                percentage = (double)completedCheckpointsCount / checkpointsCount * 100;
            }

            return percentage;
        }

        public bool IsTourProgressAbove35Percent()
        {
            double progressPercentage = CalculateTourProgressPercentage();

            return progressPercentage > 35.0;
        }

        public bool HasOneWeekPassedSinceLastActivity()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan difference = currentTime - this.LastActivity;

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
