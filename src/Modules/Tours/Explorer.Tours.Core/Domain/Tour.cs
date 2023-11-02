using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Explorer.Tours.Core.Domain
{
    public class Tour : Entity
    {
        public int AuthorId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public Demandigness? DemandignessLevel { get; init; }
        public TourStatus Status { get; private set; }
        public double Price { get; init; }
        public List<string>? Tags { get; init; }
        public List<Equipment> Equipment { get; init; }
        public List<Checkpoint> Checkpoints { get; init; }
        public List<PublishedTour>? PublishedTours { get; init; }
        public List<ArchivedTour>? ArchivedTours { get; init; }
        public List<TourTime>? TourTimes { get; private set; }

        public Tour AddEquipment(Equipment equip)
        {
            Equipment.Add(equip);
            return this;
        }

        public Tour(int authorId, string name, string? description, Demandigness? demandignessLevel, List<string>? tags, TourStatus status = TourStatus.Draft,double price=0)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (authorId == 0) throw new ArgumentException("Invalid author");
            if (price < 0) throw new ArgumentException("Invalid price");

            Name = name;
            Description = description;
            DemandignessLevel = demandignessLevel;
            Status = status;
            Price = price;
            AuthorId = authorId;
            Tags = tags;
            Equipment = new List<Equipment>();
            Checkpoints = new List<Checkpoint>();
            TourTimes = new List<TourTime>();
        }

        public bool IsForPublishing()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description) && DemandignessLevel != null && Tags != null;
        }
        private bool IsForArchiving()
        {
            return Status == TourStatus.Published;
        }

        public bool ValidateCheckpoints()
        {
            return Checkpoints != null && Checkpoints.Count() >= 2;
        }

        public bool ValidateTourTimes()
        {
            return TourTimes != null && TourTimes.Count() >= 1;
        }
        public bool Publish()
        {
            if(IsForPublishing() && ValidateCheckpoints())
            {
                Status = TourStatus.Published;
                var publishedTour = new PublishedTour(DateTime.Now);
                PublishedTours.Add(publishedTour);
                return true;
            }
            return false;
        }

        public bool Archive()
        {
            if (IsForArchiving())
            {
                Status = TourStatus.Archived;
                var archivedTour = new ArchivedTour(DateTime.Now);
                ArchivedTours.Add(archivedTour);
                return true;
            }
            return false;
        }


        public void AddTime(double time, double distance, string type)
        {
            if (TourTimes == null)
            {
                TourTimes = new List<TourTime>();

            }
            if(Enum.TryParse<TransportationType>(type, true, out var t))
            {
                var tourTime = new TourTime(time + CalculateRequiredTime(), distance, t);
                TourTimes.Add(tourTime);
            }
        }

        public void ClearTourTimes()
        {
            if (TourTimes != null)
                TourTimes.Clear();
        }

        private double CalculateRequiredTime()
        {
            if(Checkpoints != null)
            {
                return Checkpoints.Sum(n => n.RequiredTimeInSeconds);
            }
            return 0;
        }


        public Tour FilterView(Tour tour)
        {
            if (tour.Checkpoints.Count > 0) {
                Checkpoint cp = tour.Checkpoints[0];
                tour.Checkpoints.Clear();
                tour.Checkpoints.Add(cp);
            };


            return tour;
        }

    }
}

public enum TourStatus
{
    Draft,
    Published,
    Archived
}

public enum Demandigness
{
    Easy,
    Medium,
    Hard
}
