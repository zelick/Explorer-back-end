using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IMessageRepository : ICrudRepository<Message>
    {
        Message MarkAsRead(int messageId);
        List<Message> GetAllUnread(int userId);
        List<Message> GetAllSent(int userId);
        List<Message> GetAllReceived(int userId);
        Message Send(Message message);
    }
}
