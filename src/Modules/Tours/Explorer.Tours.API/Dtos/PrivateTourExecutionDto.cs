using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PrivateTourExecutionDto
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int LastVisited { get; init; }
        public bool Finished { get; init; }
    }
}
