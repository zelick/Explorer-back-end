using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ReportedIssueCommentDto
    {
        public DateTime CreationTime { get; set; }
        public int CreatorId { get; set; }
        public string Text { get; set; }
        public string PersonName { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
