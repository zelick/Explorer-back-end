using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using Explorer.Tours.Core.Domain;

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
        public DateTime StartTime { get; init; }
        public EncounterExecution() { }
        public EncounterExecution(long encounterId, Encounter encounter, long touristId, double touristLatitude, double touristLongitute, EncounterExecutionStatus status, DateTime startTime)
        {
            EncounterId = encounterId;
            Encounter = encounter;
            TouristId = touristId;
            TouristLatitude = touristLatitude;
            TouristLongitute = touristLongitute;
            Status = status;
            StartTime = startTime;
            Validate();
        }

        public void Validate()
        {
            if (EncounterId == 0)
                throw new ArgumentException("Invalid encounter Id.");
            if (Encounter == null) 
                throw new ArgumentException("Invalid encounter.");
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
        }

        public void Activate()
        {
            Status = EncounterExecutionStatus.Active;
        }
        public void Abandone()
        {
            Status = EncounterExecutionStatus.Abandoned;
        }
        public void Completed()
        {
            Status = EncounterExecutionStatus.Completed;
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
