using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class VerificationTokenDto
    {
        public long userId { get; set; }
        public DateTime tokenCreationTime { get; set; }
        public string TokenData { get; set; }
    }
}
