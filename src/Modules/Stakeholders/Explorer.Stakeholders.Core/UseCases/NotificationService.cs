using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class NotificationService : CrudService<NotificationDto, Notification>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(ICrudRepository<Notification> repository, IMapper mapper, INotificationRepository notificationRepository) : base(repository, mapper) 
        { 
            _notificationRepository = notificationRepository;
        }

        public Result<NotificationDto> AddNotification(NotificationDto notificationDto)
        {
            Notification notification = new Notification(notificationDto.Text, notificationDto.UserId, notificationDto.RequestId);
            return MapToDto(CrudRepository.Create(notification));
        }

        public Result<List<NotificationDto>> GetAllUnread(int userId)
        {
            var unreadNotifications = _notificationRepository.GetAllUnread(userId);
            return MapToDto(unreadNotifications);
        }

        public Result<NotificationDto> MarkAsRead(int id)
        {
            var notification = _notificationRepository.MarkAsRead(id);
            return MapToDto(notification);
        }
    }
}
