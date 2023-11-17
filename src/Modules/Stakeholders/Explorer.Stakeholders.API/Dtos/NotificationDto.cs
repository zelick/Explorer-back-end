using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int RequestId { get; set; }
    }
}
