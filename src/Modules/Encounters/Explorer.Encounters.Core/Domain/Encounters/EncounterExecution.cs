using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class EncounterExecution : Entity
    {
        public long EncounterId { get; init; }
        [ForeignKey("EncounterId")]
        public Encounter Encounter { get; set; }
        public long TouristId { get; init; }
        public double TouristLatitude { get; init; }
        public double TouristLongitute { get; init; }
        public EncounterExecutionStatus Status { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public EncounterExecution() { }
        public EncounterExecution(long encounterId, Encounter encounter, long touristId, double touristLatitude, double touristLongitute, EncounterExecutionStatus status, DateTime startTime, DateTime endTime)
        {
            EncounterId = encounterId;
            Encounter = encounter;
            TouristId = touristId;
            TouristLatitude = touristLatitude;
            TouristLongitute = touristLongitute;
            Status = status;
            StartTime = startTime;
            EndTime = endTime;
            Validate();
        }

        public void Validate()
        {
            if (EncounterId == 0)
                throw new ArgumentException("Invalid encounter Id.");
            //if (Encounter == null) 
            //    throw new ArgumentException("Invalid encounter.");
            if (TouristId == 0)
                throw new ArgumentException("Invalid tourist.");
            if (TouristLongitute < -180 || TouristLatitude > 180)
                throw new ArgumentException("Invalid longitude");
            if (TouristLatitude < -90 || TouristLatitude > 90)
                throw new ArgumentException("Invalid latitude");
            if (string.IsNullOrWhiteSpace(Status.ToString()))
                throw new ArgumentException("Invalid execution status.");
            if (StartTime.Date > DateTime.Now.Date)
                throw new ArgumentException("Invalid StartTime.");
            if (EndTime.Date > DateTime.Now.Date)
                throw new ArgumentException("Invalid EndTime.");
        }

        public void Activate()
        {
            Status = EncounterExecutionStatus.Active;
            this.StartTime = DateTime.UtcNow;
        }
        public void Abandone()
        {
            Status = EncounterExecutionStatus.Abandoned;
        }
        public void Completed()
        {
            Status = EncounterExecutionStatus.Completed;
            this.EndTime = DateTime.UtcNow;
        }
        
        public bool IsInRange(double touristLatitude, double touristLongitude)
        {
            double a = Math.Abs(Math.Round(TouristLongitute, 4) - Math.Round(touristLongitude, 4));
            double b = Math.Abs(Math.Round(TouristLatitude, 4) - Math.Round(touristLatitude, 4));
            return a < 0.01 && b < 0.01;
        }
        //obrisi
        public void FinishEncounter()
        {
            this.Status = EncounterExecutionStatus.Completed;
        }
    }

    public enum EncounterExecutionStatus
    {
        Pending,
        Completed, 
        Active,
        Abandoned
    }
}
