using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class CompositeTour
    {
        public int OwnerId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public Demandigness? DemandignessLevel { get; init; }
        public List<string>? Tags { get; init; }
        public List<Equipment>? Equipment { get; init; }
        public List<Checkpoint>? Checkpoints { get; init; }
    }
}
