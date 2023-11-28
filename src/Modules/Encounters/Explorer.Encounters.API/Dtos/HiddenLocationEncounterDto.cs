using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class HiddenLocationEncounterDto
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Image { get; set; }
        public double Range { get; set; }
    }
}
