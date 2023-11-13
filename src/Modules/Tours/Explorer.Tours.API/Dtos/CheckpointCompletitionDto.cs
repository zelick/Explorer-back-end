using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckpointCompletitionDto
    {
        public long TourExecutionId { get; set; }
        public long CheckpointId { get; set; }
        public DateTime CompletitionTime { get; set; }
    }
}
