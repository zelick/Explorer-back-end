using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
	public class UserDto
	{
		public long Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public String Role { get; set; }
		public bool IsActive { get; set; }
		//public List<ClubDto> Clubs { get; } = new();
	}
}
