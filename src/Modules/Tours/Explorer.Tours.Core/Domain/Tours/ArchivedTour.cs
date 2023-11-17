using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class ArchivedTour : ValueObject
    {
        public DateTime ArchivingDate { get; init; }

        public ArchivedTour() { }

        [JsonConstructor]
        public ArchivedTour(DateTime archivingDate)
        {
            ArchivingDate = archivingDate;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ArchivingDate;
        }
    }
}
