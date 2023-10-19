using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TourRating : Entity
    {
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public  int TouristId { get; init; }
        public int TourId { get; init; }
        //public DateTime TourAppointment { get; init; }
        public DateTime DateTime { get; init; }
        public string[]? Pictures { get; init; } // TO DO -> how to save images???
        public TourRating(int rating, string? comment, int touristId, int  tourId, /*  DateTime tourAppointment*/ DateTime dateTime, string[]? pictures)
        {
            if (rating == 0 || rating > 5) throw new ArgumentNullException("Invalid rating.");
            if (touristId == 0) throw new ArgumentNullException("Invalid tourist.");
            if (tourId == 0) throw new ArgumentNullException("Invalid tour.");

            Rating = rating;
            Comment = comment;
            TouristId = touristId;
            TourId = tourId;
            //TourAppointment = tourAppointment;
            DateTime = dateTime; //DateTime = DateTime.Now;
            Pictures = pictures;
        }
    }
}
