using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public enum MapObjectType 
    {
        Other,
        Restaurant,
        WC,
        Parking
    }
    public class MapObject : Entity
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public string? PictureURL { get; init; }
        public MapObjectType? Category { get; init; }
        public float? Longitude { get; init; }
        public float? Latitude { get; init; }

        public MapObject(string name, string? description, string pictureURL, MapObjectType? category, float? longitude, float? latitude)
        {
            if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            PictureURL = pictureURL;
            Category = category;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
