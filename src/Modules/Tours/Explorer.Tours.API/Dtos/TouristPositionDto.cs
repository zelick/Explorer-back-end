namespace Explorer.Tours.API.Dtos
{
    public class TouristPositionDto
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
