using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Dtos;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class Encounter:Entity
    {
        public long AuthorId { get; init; }
        public string Name { get; init; }    
        public string Description { get; init; }
        public int XP { get; init; }
        public EncounterStatus Status { get; private set; }
        public EncounterType Type { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public SocialEncounter? SocialEncounter { get; init; }
        public HiddenLocationEncounter? HiddenLocationEncounter { get; init;}
        public List<CompletedEncounter>? CompletedEncounter { get; init; }

        public Encounter() { }

        public Encounter(long authorId,string name, string description, int xP, EncounterType type,
            double latitude, double longitude,HiddenLocationEncounterDto? hiddenLocation,SocialEncounterDto? socialEncounter)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description");
            if (authorId==0) throw new ArgumentException("Invalid author");
            if (xP < 0) throw new ArgumentException("Invalid xP");
            if (latitude < -90 || latitude > 90) throw new ArgumentException("Invalid latitude");
            if(longitude < -180 || longitude>180) throw new ArgumentException("Invalid longitude");

            AuthorId = authorId;
            Name = name;
            Description = description;
            XP = xP;
            Status = EncounterStatus.Draft;
            Type = type;
            Latitude = latitude;
            Longitude = longitude;
            if (hiddenLocation != null) 
                HiddenLocationEncounter = new HiddenLocationEncounter(hiddenLocation.Longitude,hiddenLocation.Latitude,hiddenLocation.Image,hiddenLocation.Range);
            if (socialEncounter != null)
                SocialEncounter = new SocialEncounter(socialEncounter.RequiredPeople, socialEncounter.Range);
        }

        public bool ActivateSocial(double touristLongitude, double touristLatitude, int touristId)
        {
            if (SocialEncounter.ActiveTouristsIds.Contains(touristId))
                return false;
            if((Longitude - 0.5) < touristLongitude && touristLongitude < (Longitude + 0.5) &&
                (Latitude - 0.5) < touristLatitude && touristLatitude < (Latitude + 0.5))
            {
                Status = EncounterStatus.Active;
                SocialEncounter.ActiveTouristsIds.Add(touristId);
                CheckIfInRange(touristLongitude, touristLatitude, touristId);
            }
            return Status == EncounterStatus.Active;
        }

        public int CheckIfInRange(double touristLongitude, double touristLatitude, int touristId)
        {
            double distance = Math.Acos(Math.Sin(Latitude) * Math.Sin(touristLatitude) + Math.Cos(Latitude) * Math.Cos(touristLatitude) * Math.Cos(touristLongitude - Longitude)) * 6371000;
            if (distance > SocialEncounter.Range)
            {
                SocialEncounter.ActiveTouristsIds.Remove(touristId);
                return 0;
            }
            return SocialEncounter.ActiveTouristsIds.Count();
        }

        private bool IsRequiredPeopleNumber()
        {
            return SocialEncounter.ActiveTouristsIds.Count() >= SocialEncounter.RequiredPeople;
        }

        public void CompleteSocialEncounter()
        {
            if(IsRequiredPeopleNumber())
            {
                foreach(var touristId in SocialEncounter.ActiveTouristsIds)
                {
                    CompletedEncounter completedEncounter = new CompletedEncounter(touristId, DateTime.UtcNow);
                    CompletedEncounter.Add(completedEncounter);
                }
                SocialEncounter.ActiveTouristsIds.Clear();
            }
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
