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
        public List<TourRating> TourRatings { get; init; }

        public Tour AddEquipment(Equipment equipment)
        {
            if (!Equipment.Contains(equipment))
                Equipment.Add(equipment);
            else
                throw new ArgumentException("This equipment is already added.");
            return this;
        }

        public Tour RemoveEquipment(Equipment equipment)
        {
            if(Equipment.Contains(equipment))
                Equipment.Remove(equipment);
            else
                throw new ArgumentException("This equipment doesn't exist in list.");
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
            TourRatings=new List<TourRating>();
        }

        public bool IsForPublishing()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description) && DemandignessLevel != null && Tags != null && ValidateTourTimes();
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

        public TourPreview FilterView(Tour tour)
        {
            TourPreview result = null;
            if (tour.Checkpoints.Count > 0) {
                result = new TourPreview(tour);
            };
            return result;
        }
        
        public PurchasedTourPreview FilterPurchasedTour(Tour tour)
        {
            PurchasedTourPreview result = null;
            //neki if???
            result = new PurchasedTourPreview(tour);
            return result;
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
