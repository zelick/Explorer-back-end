using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        Message MarkAsRead(int messageId);
        List<Message> GetAllUnread(int userId);
        Message Send(Message message);
    }
}
