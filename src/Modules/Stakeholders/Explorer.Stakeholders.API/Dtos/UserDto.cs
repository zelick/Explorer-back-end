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
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleUser Role { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string VerificationToken { get;  set; }
        public bool IsVerified { get;  set; }
    }

    public enum RoleUser
    {
        Administrator,
        Author,
        Tourist
    }

}
