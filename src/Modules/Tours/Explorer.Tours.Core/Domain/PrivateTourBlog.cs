using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PrivateTourBlog: ValueObject
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Equipment { get; init; }
        [JsonConstructor]
        public PrivateTourBlog(string title, string description, string equipment)
        {
            Title = title;
            Description = description;
            Equipment = equipment;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return Description;
            yield return Equipment;
        }
    }
}
