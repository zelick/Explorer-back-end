using Explorer.BuildingBlocks.Core.Domain;

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

        public Encounter(long authorId,string name, string description, int xP, EncounterType type,EncounterStatus status,
            double latitude, double longitude,HiddenLocationEncounter? hiddenLocation,SocialEncounter? socialEncounter)
        {
            if (IsValid(name, description, authorId, xP, longitude, latitude,status))
            {

                AuthorId = authorId;
                Name = name;
                Description = description;
                XP = xP;
                Status = status;
                Type = type;
                Latitude = latitude;
                Longitude = longitude;
                if (hiddenLocation != null)
                    HiddenLocationEncounter = hiddenLocation;
                if (socialEncounter != null)
                    SocialEncounter = socialEncounter;
            }
        }

        public Encounter(Encounter encounter)
        {
            if (IsValid(encounter.Name, encounter.Description, encounter.AuthorId, encounter.XP, encounter.Longitude, encounter.Latitude,encounter.Status))
            {
                AuthorId = encounter.AuthorId;
                Name = encounter.Name;
                Description = encounter.Description;
                XP = encounter.XP;
                Status = EncounterStatus.Draft;
                Type = encounter.Type;
                Latitude = encounter.Latitude;
                Longitude = encounter.Longitude;
                if (encounter.HiddenLocationEncounter != null)
                    HiddenLocationEncounter = encounter.HiddenLocationEncounter;
                if (encounter.SocialEncounter != null)
                    SocialEncounter = encounter.SocialEncounter;
            }
        }

        public bool IsValid(string name,string description,long authorId,int xp,double longitude,double latitude,EncounterStatus status)
        {
            return IsNameValid(name) && IsDescriptionValid(description) && IsXpValid(xp) && IsAuthorIdValid(authorId) &&
                IsLongitudeValid(longitude) && IsLatitudeValid(latitude) && IsStatusValid(status); ;
        }

        private bool IsNameValid(string name)
        {
            bool isInvalid = string.IsNullOrWhiteSpace(name);
            if (isInvalid) throw new ArgumentException("Invalid name.");
            return !isInvalid;
        }
        private bool IsDescriptionValid(string name)
        {
            bool isInvalid = string.IsNullOrWhiteSpace(name);
            if (isInvalid) throw new ArgumentException("Invalid description.");
            return !isInvalid;
        }

        private bool IsAuthorIdValid(long authorId)
        {
            bool isInvalid = authorId==0;
            if (isInvalid) throw new ArgumentException("Invalid author.");
            return !isInvalid;
        }

        private bool IsXpValid(int xp)
        {
            bool isInvalid = xp < 0;
            if (isInvalid) throw new ArgumentException("Invalid xP");
            return !isInvalid;
        }
        public bool IsAuthor(long userId)
        {
            return AuthorId == userId;
        }

        private bool IsLongitudeValid(double longitude) 
        {
            bool isInvalid = longitude < -180 || longitude > 180;
            if (isInvalid) throw new ArgumentException("Invalid longitude");
            return !isInvalid;
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
        public bool IsLatitudeValid(double latitude)
        {
            bool isInvalid = latitude < -90 || latitude > 90;
            if (isInvalid) throw new ArgumentException("Invalid latitude");
            return !isInvalid;
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
        private bool IsStatusValid(EncounterStatus status)
        {
            bool isInvalid = (status==EncounterStatus.Active || status==EncounterStatus.Archived);
            if (isInvalid) throw new ArgumentException("Invalid status");
            return !isInvalid;
        }

    }

    public enum EncounterStatus
    {
        Draft,
        Active,
        Archived,
        Published
    }
    public enum EncounterType
    {
        Social,
        Location,
        Misc
    }

}
