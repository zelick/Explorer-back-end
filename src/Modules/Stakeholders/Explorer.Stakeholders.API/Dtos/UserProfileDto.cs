using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<UserDto> Followers { get; set; }
        public List<UserDto> Followed { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
