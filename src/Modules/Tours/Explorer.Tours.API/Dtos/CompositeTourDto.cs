using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CompositeTourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DemandignessLevel { get; set; }
        public List<string>? Tags { get; set; }
        public int OwnerId { get; set; }
        public List<EquipmentDto>? Equipment { get; set; }
        public List<CheckpointDto>? Checkpoints { get; set; }
    }
}
