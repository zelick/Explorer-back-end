using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class MessageDatabaseRepository : CrudDatabaseRepository<Message, StakeholdersContext>, IMessageRepository
    {
        public MessageDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

        public Message MarkAsRead(int messageId)
        {

            var message = DbContext.Messages.FirstOrDefault(m => m.Id == messageId);
            if (message == null) throw new KeyNotFoundException("Not found: " + messageId);
            message.MarkAsRead();
            DbContext.Messages.Update(message);
            DbContext.SaveChanges();

            return message;
        }

        public List<Message> GetAllUnread(int userId)
        {
            return DbContext.Messages.Where(m => m.RecipientId == userId && !m.IsRead).ToList();
        }

        public Message Send(Message message)
        {
            DbContext.Messages.Add(message);
            DbContext.SaveChanges();
            return message;
        }

        public List<Message> GetAllSent(int userId)
        {
            return DbContext.Messages.Where(m => m.SenderId == userId).ToList();
        }

        public List<Message> GetAllReceived(int userId)
        {
            return DbContext.Messages.Where(m => m.RecipientId == userId).ToList();
        }
    }
}
