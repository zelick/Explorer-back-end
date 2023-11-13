using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckpointSecretDto
    {
         public string Description { get; set; }
        public List<string>? Pictures { get; set; }
    }
}
