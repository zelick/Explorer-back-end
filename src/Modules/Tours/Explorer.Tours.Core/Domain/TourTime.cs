using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TourTime : ValueObject
    {
        public double TimeInSeconds { get; init; }
        public double Distance { get; init; }
        public TransportationType Transportation { get; init; }

        [JsonConstructor]
        public TourTime(double timeInSeconds, double distance, TransportationType transportation)
        {
            TimeInSeconds = timeInSeconds;
            Distance = distance;
            Transportation = transportation;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TimeInSeconds;
            yield return Distance;
            yield return Transportation;
        }
    }

    public enum TransportationType
    {
        walking,
        driving,
        cycling
    }
}
