using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public RoleUser Role { get; private set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
    }
    public enum RoleUser
    {
        Administrator,
        Author,
        Tourist
    }
}
