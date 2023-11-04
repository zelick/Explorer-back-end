using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TourPreview
    {
        public long Id { get; init; }
        public int AuthorId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public Demandigness? DemandignessLevel { get; init; }
        public double Price { get; init; }
        public List<string>? Tags { get; init; }
        public List<Equipment> Equipment { get; init; }
        public CheckpointPreview Checkpoint { get; init; }

        public List<TourRatingPreview> TourRatings { get; init; }

        public List<TourTime> TourTimes { get; init; }

        public TourPreview(Tour tour)
        {
            Id = tour.Id;
            AuthorId = tour.AuthorId;
            Name = tour.Name;
            Description = tour.Description;
            DemandignessLevel = tour.DemandignessLevel;
            Price = tour.Price;
            Tags = tour.Tags;
            Equipment = tour.Equipment;
            Checkpoint = new CheckpointPreview(tour.Checkpoints[0]);
            TourTimes = tour.TourTimes;
            TourRatings= new List<TourRatingPreview>();
            foreach(TourRating tourRating in tour.TourRatings)
            {
                TourRatings.Add(new TourRatingPreview(tourRating));
            }
        }
    }
}
