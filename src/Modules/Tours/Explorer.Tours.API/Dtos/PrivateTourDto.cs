using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PrivateTourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TouristId { get; set; }
        public List<PublicCheckpointDto> Checkpoints { get; set; }
    }
}
