using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounter : Encounter
    {
        public int RequiredPeople { get; init; }
        public double Range { get; init; }  
        public List<int>? ActiveTouristsIds { get; init; }
        public SocialEncounter() { }

        public SocialEncounter(SocialEncounter socialEncounter)
        {
        }

        public bool ActivateSocial(double touristLongitude, double touristLatitude, int touristId)
        {
            if (ActiveTouristsIds.Contains(touristId))
                return false;
            if ((Longitude - 0.5) < touristLongitude && touristLongitude < (Longitude + 0.5) &&
                (Latitude - 0.5) < touristLatitude && touristLatitude < (Latitude + 0.5))
            {
                Status = EncounterStatus.Active;
                ActiveTouristsIds.Add(touristId);
                CheckIfInRange(touristLongitude, touristLatitude, touristId);
            }
            return Status == EncounterStatus.Active;
        }

        public int CheckIfInRange(double touristLongitude, double touristLatitude, int touristId)
        {
            double distance = Math.Acos(Math.Sin(Latitude) * Math.Sin(touristLatitude) + Math.Cos(Latitude) * Math.Cos(touristLatitude) * Math.Cos(touristLongitude - Longitude)) * 6371000;
            if (distance > Range)
            {
                ActiveTouristsIds.Remove(touristId);
                return 0;
            }
            return ActiveTouristsIds.Count();
        }

        private bool IsRequiredPeopleNumber()
        {
            return ActiveTouristsIds.Count() >= RequiredPeople;
        }

        public void CompleteSocialEncounter()
        {
            if (IsRequiredPeopleNumber())
            {
                foreach (var touristId in ActiveTouristsIds)
                {
                    CompletedEncounter completedEncounter = new CompletedEncounter(touristId, DateTime.UtcNow);
                    CompletedEncounter.Add(completedEncounter);
                }
                ActiveTouristsIds.Clear();
            }
        }


    }
}
