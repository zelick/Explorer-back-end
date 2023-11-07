using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TourExecution:Entity
    {
        public long TouristId { get; init; }
        public long TourId { get; init; }
        public Tour Tour { get; init; }
        public DateTime Start {  get; init; }
        public  DateTime LastActivity { get; init; }
        public ExecutionStatus ExecutionStatus { get; init; }

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

        }

        public void RegisterActivity(TouristPosition touristPosition)
        {
            
        }

    }

    public enum ExecutionStatus
    {
        Completed,
        Abandoned,
        InProgress
    }


}
