using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        // CRUD
        Result<NotificationDto> Create(NotificationDto notificationDto);
        Result<NotificationDto> Get(int id);
        Result<NotificationDto> Update(NotificationDto notificationDto);
        Result Delete(int id);

        // other
        //Result<NotificationDto> CreateNotification(string description, long userId);
        //Result<NotificationDto> CreateRequestNotification(string description, long userId, long? foreignId);
        //Result<NotificationDto> CreateReportedIssueNotification(string description, long userId, long? foreignId);
        Result<PagedResult<NotificationDto>> GetAllByUser(long id, int page, int pageSize);
        Result<PagedResult<NotificationDto>> GetUnreadByUser(long id, int page, int pageSize);
    }
}
