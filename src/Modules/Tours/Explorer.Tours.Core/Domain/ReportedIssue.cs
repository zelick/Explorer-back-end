using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
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
        public int TourId { get; init; }
        public int TouristId { get; init; }

        public ReportedIssue(string category, string? description, int priority, DateTime time, int tourId, int touristId)
        {
            if (string.IsNullOrWhiteSpace(category) || priority == 0 || tourId == 0) throw new ArgumentException("Invalid reported issue.");
            Category = category;
            Description = description;
            Priority = priority;
            Time = time;
            TourId = tourId;
            TouristId = touristId;
        }

    }
}
