using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime? SentDateTime { get;  set; }
        public DateTime? ReadDateTime { get;  set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}
