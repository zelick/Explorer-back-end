using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalNotificationService
    {
        NotificationDto CreateNotification(string description, long userId);
        NotificationDto CreateWalletNotification(string description, long userId);
        NotificationDto CreateReportedIssueNotification(string description, long userId, long? foreignId);
    }
}
