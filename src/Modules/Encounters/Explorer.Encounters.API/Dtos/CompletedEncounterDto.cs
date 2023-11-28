using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class CompletedEncounterDto
    {
        public long TouristId { get; set; }
        public DateTime Completition { get; set; }
    }
}
