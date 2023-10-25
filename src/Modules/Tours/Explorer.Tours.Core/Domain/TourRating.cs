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
        public string[]? Pictures { get; init; } // TO DO -> upload picture file
        [ForeignKey("TourId")]
        public Tour Tour { get; set; }
        public TourRating() { }

        public TourRating(int rating, string? comment, int touristId, int tourId, Tour tour, DateTime tourDate, DateTime creationDate, string[]? pictures)
        //public TourRating(int rating, string? comment, int touristId, int tourId, DateTime tourDate, DateTime creationDate, string[]? pictures)
        {
            if (rating == 0 || rating > 5) throw new ArgumentNullException("Invalid rating.");
            if (touristId == 0) throw new ArgumentNullException("Invalid tourist.");
            if (tourId == 0) throw new ArgumentNullException("Invalid tour.");
            //if (creationDate > DateTime.Now.Date) throw new ArgumentNullException("Invalid creation time.");
            //if (TourDate > DateTime.Now.Date) throw new ArgumentNullException("Invalid tour date.");

            Rating = rating;
            Comment = comment;
            TouristId = touristId;
            TourId = tourId;
            TourDate = tourDate;
            CreationDate = creationDate;
            Pictures = pictures;
            Tour = tour;
        }
    }
}
