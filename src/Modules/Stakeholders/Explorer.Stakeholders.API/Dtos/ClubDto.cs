using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long TouristId { get; set; }
        //public List<UserDto> Tourists { get; } = new();
        public List<UserDto> Users { get; set; }
	}
}