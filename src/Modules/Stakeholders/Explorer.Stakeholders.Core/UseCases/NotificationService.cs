using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class NotificationService : CrudService<NotificationDto, Notification>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(ICrudRepository<Notification> repository, IMapper mapper, INotificationRepository notifRepo) : base(repository, mapper)
        {
            _notificationRepository = notifRepo;
        }

        public Result<NotificationDto> Create(NotificationDto notificationDto)
        {
            try
            {
                if (!IsNewNotifValid(notificationDto))
                    return Result.Fail("Description shouldn't be empty!");

                if (!Enum.TryParse(notificationDto.Type, true, out NotificationType type))
                    throw new ArgumentException("Invalid notification type.", nameof(notificationDto.Type));
                Notification notification = new Notification(notificationDto.Description, notificationDto.CreationTime, type, notificationDto.IsRead, notificationDto.UserId, notificationDto.ForeignId);

                return MapToDto(_notificationRepository.Create(notification));
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        private bool IsNewNotifValid(NotificationDto notification)
        {
            return (!string.IsNullOrWhiteSpace(notification.Description));
        }

        public Result<PagedResult<NotificationDto>> GetAllByUser(long id, int page, int pageSize)
        {
            try
            {
                var result = _notificationRepository.GetAllByUser(id);
                var paged = new PagedResult<Notification>(result, result.Count());
                return MapToDto(paged);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<NotificationDto>> GetUnreadByUser(long id, int page, int pageSize)
        {
            try
            {
                var result = _notificationRepository.GetUnreadByUser(id);
                var paged = new PagedResult<Notification>(result, result.Count());
                return MapToDto(paged);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
