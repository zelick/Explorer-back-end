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
        public List<CompletedEncounter>? CompletedEncounter { get; private set; } = new List<CompletedEncounter>();

        public Encounter() { }

        public Encounter(long authorId,string name, string description, int xP, EncounterType type,EncounterStatus status,
            double latitude, double longitude)
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
                CompletedEncounter = new List<CompletedEncounter>();
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

        public void MakeEncounterPublished()
        {
            Status = EncounterStatus.Published;
        }

        private bool IsLongitudeValid(double longitude) 
        {
            bool isInvalid = longitude < -180 || longitude > 180;
            if (isInvalid) throw new ArgumentException("Invalid longitude");
            return !isInvalid;
        }

        private bool IsStatusValid(EncounterStatus status)
        {
            bool isInvalid = (status==EncounterStatus.Active || status==EncounterStatus.Archived);
            if (isInvalid) throw new ArgumentException("Invalid status");
            return !isInvalid;
        }

        public void FinishEncounter(long toruistId)
        {
           this.Status = EncounterStatus.Archived;
           if (this.CompletedEncounter == null)
           { 
                this.CompletedEncounter = new List<CompletedEncounter>();
           }
           CompletedEncounter ce = new CompletedEncounter(toruistId, DateTime.Now);
           this.CompletedEncounter.Add(ce);
        }
        public bool IsLatitudeValid(double latitude)
        {
            bool isInvalid = latitude < -90 || latitude > 90;
            if (isInvalid) throw new ArgumentException("Invalid latitude");
            return !isInvalid;
        }

        public double GetDistanceFromEncounter(double longitude, double latitude)
        {
            return Math.Acos(Math.Sin(Math.PI / 180 * (Latitude)) * Math.Sin(Math.PI / 180 * latitude) + Math.Cos(Math.PI / 180 * Latitude) * Math.Cos(Math.PI / 180 * latitude) * Math.Cos(Math.PI / 180 * Longitude - Math.PI / 180 * longitude)) * 6371000;
        }

        public bool IsCloseEnough(double longitude, double latitude)
        {
            return GetDistanceFromEncounter(longitude, latitude) <= 1000;
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
