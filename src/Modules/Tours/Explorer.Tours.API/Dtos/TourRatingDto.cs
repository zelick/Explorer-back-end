namespace Explorer.Tours.API.Dtos
{
    public class TourRatingDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
        //public DateTime TourAppointment { get; set; }
        public DateTime DateTime { get; set; }
        public string[]? Pictures { get; set; } // TO DO -> how to save images???
    }
}
