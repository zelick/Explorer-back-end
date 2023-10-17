using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TourPreference : Entity
    {
        public int Id { get; init; }
        public int CreatorId { get; init; }
        public TourDifficulty? Difficulty { get; init; }
        public int Walk { get; init; } = 0;
        public int Bike { get; init; } = 0;
        public int Car { get; init; } = 0;
        public int Boat { get; init; } = 0;
        public List<string>? Tags { get; init; }

        public TourPreference(int id, int creatorId, TourDifficulty? difficulty, int walk, int bike, int car, int boat, List<string>? tags)
        {
            if (id == 0) throw new ArgumentException("Invalid preference");
            if (creatorId == 0) throw new ArgumentException("Inavlid creator");
            CreatorId = creatorId;
            Difficulty = difficulty;
            Walk = walk;
            Bike = bike;
            Car = car;
            Boat = boat;
            Tags = tags;
        }
    }
    public enum TourDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
}
