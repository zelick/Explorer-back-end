using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class Encounter:Entity
    {
        public string Name { get; init; }    
        public string Description { get; init; }
        public int XP { get; init; }
        public EncounterStatus Status { get; init; }
        public EncounterType Type { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public SocialEncounter? SocialEncounter { get; init; }
        public HiddenLocationEncounter? HiddenLocationEncounter { get; init;}

        public Encounter() { }

        public Encounter(string name, string description, int xP, EncounterStatus? status, EncounterType type, double latitude, double longitude)
        {
            Name = name;
            Description = description;
            XP = xP;
            Status = EncounterStatus.Draft;
            Type = type;
            Latitude = latitude;
            Longitude = longitude;

        }
    }
    public enum EncounterStatus
    {
        Draft,
        Active,
        Archived
    }
    public enum EncounterType
    {
        Social,
        Location,
        Misc
    }

}
