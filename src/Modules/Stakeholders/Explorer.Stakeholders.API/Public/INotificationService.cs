using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        // CRUD
        Result<NotificationDto> Get(int id);
        Result<NotificationDto> Update(NotificationDto notificationDto);
        Result Delete(int id);

        // other
        Result<NotificationDto> Create(string description, long userId, long? foreignId, int type);
        Result<PagedResult<NotificationDto>> GetAllByUser(long id, int page, int pageSize);
        Result<PagedResult<NotificationDto>> GetUnreadByUser(long id, int page, int pageSize);
    }
}
