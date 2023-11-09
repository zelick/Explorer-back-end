using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionDto
    {
        public long TouristId { get; set; }
        public PurchasedTourPreviewDto Tour { get; set; }
        public DateTime Start { get; set; }
        public DateTime LastActivity { get; set; }
        public string ExecutionStatus { get; set; }
    }
}
