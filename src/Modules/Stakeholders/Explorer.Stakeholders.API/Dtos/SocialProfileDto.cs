using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class SocialProfileDto
    {
        public int Id { get; set; }
        public List<UserDto> Followers { get; set; }
        public List<UserDto> Followed { get; set; }
    }
}
