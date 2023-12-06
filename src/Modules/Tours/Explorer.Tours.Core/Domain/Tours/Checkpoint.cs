using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Checkpoint : Entity
    {
        public long TourId { get; init; }
        public Tour? Tour { get; init; }
        public int AuthorId { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<string> Pictures { get; init; }
        public double RequiredTimeInSeconds { get; init; }
        public CheckpointSecret? CheckpointSecret { get; private set; }
        public long EncounterId { get; set; }
        public bool IsSecretPrerequisite { get; set; }



        public Checkpoint(long tourId, int authorId, double longitude, double latitude, string name, string description, List<string> pictures, double requiredTimeInSeconds)
        {
            if (tourId == 0) throw new ArgumentException("Invalid Tour ID");
            TourId = tourId;
            AuthorId = authorId;
            Longitude = longitude;
            Latitude = latitude;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Pictures = new List<string>();
            if (pictures.Count() > 0)
                Pictures = pictures ?? throw new ArgumentNullException(nameof(pictures));
            else throw new ArgumentException("Invalid Picture");
            RequiredTimeInSeconds = requiredTimeInSeconds;
            EncounterId = 0;
            IsSecretPrerequisite = false;
        }

        public Checkpoint CreateCheckpointSecret(string description, List<string> pictures)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            {
                CheckpointSecret = new CheckpointSecret(description, pictures);
            }
            return this;
        }

        public Checkpoint UpdateCheckpointSecret(string description, List<string> pictures)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            {
                CheckpointSecret = new CheckpointSecret(description, pictures);
            }
            return this;
        }

        public Checkpoint DeleteCheckpointSecret()
        {
            CheckpointSecret = null;
            return this;
        }

        public bool IsAuthor(int userId)
        {
            return AuthorId == userId;
        }

    }
}
