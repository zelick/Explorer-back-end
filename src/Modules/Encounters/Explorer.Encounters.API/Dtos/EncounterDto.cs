using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterDto
    {
        public long AuthorId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XP { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public SocialEncounterDto? SocialEncounter { get; set; }
        public HiddenLocationEncounterDto? HiddenLocationEncounter { get; set; }   
        public List<CompletedEncounterDto>? CompletedEncounter { get; set; }
        
    }
}
