using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicMapObject : Entity
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public string? PictureURL { get; init; }
        public MapObjectType? Category { get; init; }
        public float? Longitude { get; init; }
        public float? Latitude { get; init; }

        public PublicMapObject(string name, string? description, string pictureURL, MapObjectType? category, float? longitude, float? latitude)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            PictureURL = pictureURL;
            Category = category;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
