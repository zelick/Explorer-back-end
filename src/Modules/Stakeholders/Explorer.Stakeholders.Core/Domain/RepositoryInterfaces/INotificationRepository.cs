using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        List<Notification> GetAllUnread(int userId);
        Notification MarkAsRead(int id);
        Notification AddNotification(Notification notification);
    }
}
