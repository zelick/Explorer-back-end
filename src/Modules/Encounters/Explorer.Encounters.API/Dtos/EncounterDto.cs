using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XP { get; set; }
        public string? EncounterStatus { get; set; }
        public string EncounterType { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public SocialEncounterDto? SocialEncounter { get; set; }
        public HiddenLocationEncounterDto? HiddenLocationEncounter { get; set; }   
        public List<CompletedEncounterDto>? CompletedEncounter { get; set; }
        
    }
}
