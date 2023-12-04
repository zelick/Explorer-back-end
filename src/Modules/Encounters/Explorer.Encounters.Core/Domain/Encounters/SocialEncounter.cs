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
        public int CheckIfInRange(double touristLongitude, double touristLatitude, int touristId)
        {
            double distance = Math.Acos(Math.Sin(Math.PI/180*(Latitude)) * Math.Sin(Math.PI / 180 * touristLatitude) + Math.Cos(Math.PI / 180 * Latitude) * Math.Cos(Math.PI / 180 * touristLatitude) * Math.Cos(Math.PI / 180 * Longitude - Math.PI / 180 * touristLongitude)) * 6371000;
            if (distance > Range)
            {
                RemoveTourist(touristId);
                return 0;
            }
            else AddTourist(touristId);
            return ActiveTouristsIds.Count();
        }

        private void AddTourist(int touristId)
        {
            if(ActiveTouristsIds != null && !ActiveTouristsIds.Contains(touristId))
                ActiveTouristsIds.Add(touristId);
        }

        private void RemoveTourist(int touristId)
        {
            if (ActiveTouristsIds != null && ActiveTouristsIds.Contains(touristId))
                ActiveTouristsIds.Remove(touristId);
        }
        public bool IsRequiredPeopleNumber()
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
