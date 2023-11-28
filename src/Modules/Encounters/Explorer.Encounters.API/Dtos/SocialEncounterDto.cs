using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class SocialEncounterDto
    {
        public int RequiredPeople { get; set; }
        public double Range { get; set; }
        public List<int> ActiveTouristsIds { get; set; }
    }
}
