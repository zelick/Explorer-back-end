using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ObjectRequestDto
    {
        public int Id { get; set; }
        public int MapObjectId { get; set; }
        public int AuthorId { get; set; }
        public string Status { get; set; }
    }
}
