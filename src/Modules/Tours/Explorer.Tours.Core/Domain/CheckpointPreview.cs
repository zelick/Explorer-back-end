using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain
{
    public class CheckpointPreview
    {
        public long Id { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<string> Pictures { get; init; }
        public double RequiredTimeInSeconds { get; init; }

        public CheckpointPreview(Checkpoint checkpoint)
        {
            Id = checkpoint.Id;
            Longitude = checkpoint.Longitude;
            Latitude = checkpoint.Latitude;
            Name = checkpoint.Name;
            Description = checkpoint.Description;
            Pictures = checkpoint.Pictures;
            RequiredTimeInSeconds = checkpoint.RequiredTimeInSeconds;
        }
    }
}
