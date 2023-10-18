using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TourPreference : Entity
    {
        public int CreatorId { get; init; }
        public TourDifficulty? Difficulty { get; init; }
        public int Walk { get; init; }
        public int Bike { get; init; }
        public int Car { get; init; }
        public int Boat { get; init; }
        public List<string>? Tags { get; init; }

        public TourPreference(int creatorId, TourDifficulty? difficulty, int walk, int bike, int car, int boat, List<string>? tags)
        {
            if (walk < 0 || walk > 3) throw new ArgumentException("Invalid value for Walk. It should be between 0 and 3.");
            if (bike < 0 || bike > 3) throw new ArgumentException("Invalid value for Bike. It should be between 0 and 3.");
            if (car < 0 || car > 3) throw new ArgumentException("Invalid value for Car. It should be between 0 and 3.");
            if (boat < 0 || boat > 3) throw new ArgumentException("Invalid value for Boat. It should be between 0 and 3.");

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
