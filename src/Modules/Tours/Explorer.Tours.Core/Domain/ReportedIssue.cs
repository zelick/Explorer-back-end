using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class ReportedIssue : Entity
    {
        public string Category { get; init; }
        public string? Description { get; init; }
        public int Priority { get; init; }
        public DateTime Time { get; init; }
        public long TourId { get; init; }
        public bool Resolved { get; private set; } // indicates if the problem is resolved or not
        public int TouristId { get; init; }
        [ForeignKey("TourId")]
        public Tour Tour { get; set; }
        public virtual ICollection<ReportedIssueComment> ?Comments { get; set; }
        public ReportedIssue() { }
        public ReportedIssue(string category, string? description, int priority, DateTime time, int tourId, int touristId, Tour tour, bool resolved)
        {
            if (string.IsNullOrWhiteSpace(category) || priority == 0 || tourId == 0) throw new ArgumentException("Invalid reported issue.");
            Category = category;
            Description = description;
            Priority = priority;
            Time = time;
            TourId = tourId;
            TouristId = touristId;
            Tour= tour;
            Resolved = resolved;
            Comments= new List<ReportedIssueComment>();
        }

        public bool IsUnresolvedWithinFiveDays()
        {
            return (!Resolved) && (DateTime.Now.AddDays(-5) > Time);
        }
        public void Resolve()
        {
            Resolved = true;
        }
    }
}
