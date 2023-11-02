using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class PublishedTour : ValueObject
    {
        public DateTime PublishingDate {  get; init; }

        public PublishedTour() { }

        [JsonConstructor]
        public PublishedTour(DateTime publishingDate) 
        {
            PublishingDate = publishingDate;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PublishingDate;
        }
    }
}
