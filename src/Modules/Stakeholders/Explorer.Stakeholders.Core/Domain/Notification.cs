using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Notification : Entity
    {
        public bool IsRead { get; private set; }
        public string Text { get; private set; }
        public int UserId { get; private set; }
        public int RequestId { get; private set; }

        public Notification(string text, int userId, int requestId)
        {
            IsRead = false;
            Text = text;
            UserId = userId;
            RequestId = requestId;
        }

        public void MarkAsRead() 
        { 
            IsRead = true;
        }
    }
}
