using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class PrivateTourExecution: ValueObject
    {
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public int LastVisited { get; private set; }
        public bool Finished { get; private set; }

        [JsonConstructor]
        public PrivateTourExecution(DateTime startDate, DateTime? endDate, int last, bool finished)
        {
            StartDate = startDate;
            EndDate = endDate;
            LastVisited = last;
            Finished = finished;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
            yield return LastVisited;
            yield return Finished;
        }
        public void Next()
        {
            LastVisited++;
        }
        public void Finish()
        {
            Finished = true;
            EndDate = DateTime.Now;
        }
    }
}
