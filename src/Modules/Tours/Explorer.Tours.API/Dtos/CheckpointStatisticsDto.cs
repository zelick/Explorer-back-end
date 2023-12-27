using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckpointStatisticsDto
    {
        public long CheckpointId { get; set; }
        public String CheckpointName { get; set; }  
        public double ArrivalPercentage { get; set; } = 0;
        public double FinishEcnounterPercentage { get; set; } = 0;
        public CheckpointStatisticsDto(long checkpointId,string checkpointName, double arrivalPercentage, double finishEcnounterPercentage) { 
            this.CheckpointId = checkpointId; 
            this.CheckpointName = checkpointName;
            this.ArrivalPercentage = arrivalPercentage;
            this.FinishEcnounterPercentage = finishEcnounterPercentage; 
        }
    }
}
