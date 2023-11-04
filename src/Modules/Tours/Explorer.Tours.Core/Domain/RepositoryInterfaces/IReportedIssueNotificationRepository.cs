using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IReportedIssueNotificationRepository : ICrudRepository<ReportedIssueNotification>
    {
        ReportedIssueNotification Create(long userId, long reportedIssueId);
        List<ReportedIssueNotification> GetAllByUser(long id);
        List<ReportedIssueNotification> GetUnreadByUser(long id);
    }
}