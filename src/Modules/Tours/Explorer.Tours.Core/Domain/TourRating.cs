using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.Core.Domain
{
    public class TourRating : Entity
    {
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public  int TouristId { get; init; }
        public long TourId { get; init; }
        public DateTime TourDate { get; init; }
        public DateTime CreationDate { get; init; }
        public List<string>? ImageNames { get; private set; }

        [ForeignKey("TourId")]
        public Tour Tour { get; set; }
        public TourRating() { }

        public TourRating(int rating, string? comment, int touristId, int tourId, Tour tour, DateTime tourDate, DateTime creationDate, List<string>? imageNames)            
        {
            Rating = rating;
            Comment = comment;
            TouristId = touristId;
            TourId = tourId;
            TourDate = tourDate;
            CreationDate = creationDate;
            ImageNames = imageNames;
            Tour = tour;
            Validate();
        }

        private void Validate()
        {
            if (Rating == 0 || Rating > 5) throw new ArgumentNullException("Invalid rating.");
            if (TouristId == 0) throw new ArgumentNullException("Invalid tourist.");
            if (TourId == 0) throw new ArgumentNullException("Invalid tour.");
        }
    }
}
