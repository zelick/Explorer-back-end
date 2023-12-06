using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository : ICrudRepository<Notification>
    {
        Notification CreateRequestNotification(string description, long userId, long? foreignId);
        Notification CreateReportedIssueNotification(string description, long userId, long? foreignId);
        Notification CreateMessageNotification(string description, long userId, long? foreignId);
        Notification CreateNotification(string description, long userId);
        Notification CreatePaymentNotification(Notification notification);
        List<Notification> GetAllByUser(long id);
        List<Notification> GetUnreadByUser(long id);
    }
}
