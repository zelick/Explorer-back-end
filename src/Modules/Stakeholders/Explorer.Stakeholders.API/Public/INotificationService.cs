using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        Result<NotificationDto> AddNotification(NotificationDto notificationDto);
        Result<List<NotificationDto>> GetAllUnread(int userId);
        Result<NotificationDto> MarkAsRead(int id);
    }
}
