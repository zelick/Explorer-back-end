using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicCheckpoint : Entity
    {
        public long TourId { get; init; }
        public Tour Tour { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<string> Pictures { get; init; }

        public PublicCheckpoint(long tourId, double longitude, double latitude, string name, string description, List<string> pictures)
        {
            if (tourId == 0) throw new ArgumentException("Invalid Tour ID");
            TourId = tourId;
            Longitude = longitude;
            Latitude = latitude;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            if (pictures.Count() > 0)
                Pictures = pictures ?? throw new ArgumentNullException(nameof(pictures));
            else throw new ArgumentException("Invalid Picture");
        }

    }
}
