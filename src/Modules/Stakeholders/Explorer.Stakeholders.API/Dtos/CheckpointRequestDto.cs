using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class CheckpointRequestDto
    {
        public int Id { get; set; }
        public long CheckpointId { get; set; }
        public int AuthorId { get; set; }
        public string Status { get; set; }
    }
}
