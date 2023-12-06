using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CompositeTourDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long OwnerId { get; set; }
        public string? DemandignessLevel { get; set; }
        public List<EquipmentDto>? Equipment { get; set; }
        public List<CheckpointDto>? Checkpoints { get; set; }

        public CompositeTourDto(long id, string name, string? description, long ownerId, string? demandignessLevel, List<EquipmentDto>? equipment, List<CheckpointDto>? checkpoints)
        {
            Id = id;
            Name = name;
            Description = description;
            OwnerId = ownerId;
            DemandignessLevel = demandignessLevel;
            Equipment = equipment;
            Checkpoints = checkpoints;

        }
    }

    
}
