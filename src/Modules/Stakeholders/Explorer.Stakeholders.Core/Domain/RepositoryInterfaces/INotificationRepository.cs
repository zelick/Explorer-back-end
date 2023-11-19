using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository : ICrudRepository<Notification>
    {
        Notification Create(string description, long userId, long? foreignId, int type);
        List<Notification> GetAllByUser(long id);
        List<Notification> GetUnreadByUser(long id);
    }
}
