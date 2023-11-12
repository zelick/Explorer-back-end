using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class CheckpointSecret : ValueObject
    {
        public string Description { get; init; }
        public List<string>? Pictures { get; init; }

        public CheckpointSecret() { }

        [JsonConstructor]
        public CheckpointSecret(string description, List<string>? pictures)
        {
            Description = description;
            Pictures = pictures;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
            yield return Pictures;
        }


    }
}
