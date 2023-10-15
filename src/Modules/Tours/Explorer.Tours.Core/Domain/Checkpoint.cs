using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class Checkpoint : Entity
    {
        public int TourID { get; init; }
        public int OrderNumber { get; init; }
        public int Longitude { get; init; }
        public int Latitude { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public string Picture { get; init; }

        public Checkpoint(int tourID, int orderNumber, int longitude, int latitude, string name, string description, string picture)
        {
            if (tourID < 0) throw new ArgumentException("Invalid Tour ID");
            if (longitude < 0 || latitude < 0) throw new ArgumentException("Invalid location coordinates");
            TourID = tourID;
            if (orderNumber == 0)
                OrderNumber = 1;
            else
                OrderNumber = orderNumber;
            Longitude = longitude;
            Latitude = latitude;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Picture = picture ?? throw new ArgumentNullException(nameof(picture));
        }
    }
}
