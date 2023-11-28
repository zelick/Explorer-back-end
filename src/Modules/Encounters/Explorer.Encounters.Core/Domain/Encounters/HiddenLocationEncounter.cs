using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class HiddenLocationEncounter:ValueObject
    {
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public string Image {  get; init; }
        public double Range { get; init; }
        public HiddenLocationEncounter() { }

        [JsonConstructor]
        public HiddenLocationEncounter(double longitude,double latitude,string image,double range)
        {
            Longitude = longitude;
            Latitude = latitude;
            Image = image;
            Range = range;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
            yield return Image;
            yield return Range;
        }
    }
}
