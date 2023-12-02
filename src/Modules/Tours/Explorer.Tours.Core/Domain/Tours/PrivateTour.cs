using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class PrivateTour:Entity
    {
        public int ToursitId { get; init; }
        public string Name { get; init; }
        public List<PublicCheckpoint> Checkpoints { get; init; }
        public PrivateTour(int touristId, string name, List<PublicCheckpoint> checkpoints)
        {
            Name = name;
            ToursitId = touristId;
            Checkpoints= checkpoints;
        }
        public PrivateTour() { }    
    }
}
