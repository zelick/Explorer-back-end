namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class HiddenLocationEncounter:Encounter
    {
        public double LocationLongitude { get; init; }
        public double LocationLatitude { get; init; }
        public string Image {  get; init; }
        public double Range { get; init; }
        public HiddenLocationEncounter() { }

        // check if encounter can be activated - tourist is in allowed range
        public bool CheckIfInRangeLocation(double touristLongitude, double touristLatitude)
        {
            double distance = Math.Acos(Math.Sin(Math.PI / 180 * (Latitude)) * Math.Sin(Math.PI / 180 * touristLatitude) + Math.Cos(Math.PI / 180 * Latitude) * Math.Cos(Math.PI / 180 * touristLatitude) * Math.Cos(Math.PI / 180 * Longitude - Math.PI / 180 * touristLongitude)) * 6371000;
            if (distance <= Range)
                return true;

            return false;
        }

        // check if all conditions for completing a hidden location encounter are met
        public bool CheckIfLocationFound(double touristLongitude, double touristLatitude)
        {
            // check if the tourist is in 5 meter range of the place where the picture is taken
            // TODO -> tourist needs to stay in 5 meter range of correct place for 30 seconds !!!
            double distance = Math.Acos(Math.Sin(Math.PI / 180 * (LocationLatitude)) * Math.Sin(Math.PI / 180 * touristLatitude) + Math.Cos(Math.PI / 180 * Latitude) * Math.Cos(Math.PI / 180 * touristLatitude) * Math.Cos(Math.PI / 180 * LocationLongitude - Math.PI / 180 * touristLongitude)) * 6371000;
            if (distance <= 5.0)
                return true;

            return false;
        }

    }
}
