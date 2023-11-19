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

        public Result<NotificationDto> Create(string description, long userId, long? foreignId, int type)
        {
            try
            {
                var result = _notificationRepository.Create(description, userId, foreignId, type);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
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
