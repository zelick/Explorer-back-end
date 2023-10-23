namespace Explorer.Tours.API.Dtos
{
    public class TourPreferenceDto
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string? Difficulty { get; set; }
        public int Walk { get; set; }
        public int Bike { get; set; }
        public int Car { get; set; }
        public int Boat { get; set; }
        public List<string>? Tags { get; set; }

    }
}
