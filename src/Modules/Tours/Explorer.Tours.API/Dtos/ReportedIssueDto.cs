using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ReportedIssueDto
    {
        public int Id { get; set; } 
        public string Category { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public DateTime Time { get; set; }
        public int TourId { get; set; }
        public int TouristId { get; set; }
        [ForeignKey("TourId")]
        public TourDto Tour { get; set; }
    }
}
