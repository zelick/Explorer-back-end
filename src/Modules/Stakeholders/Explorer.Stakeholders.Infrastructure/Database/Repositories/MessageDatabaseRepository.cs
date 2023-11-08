using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class MessageDatabaseRepository : IMessageRepository
    {
        private readonly StakeholdersContext _dbContext;

        public MessageDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Message MarkAsRead(int messageId)
        {

            var message = _dbContext.Messages.FirstOrDefault(m => m.Id == messageId);
            if (message == null) throw new KeyNotFoundException("Not found: " + messageId);
            message.MarkAsRead();
            _dbContext.Messages.Update(message);
            _dbContext.SaveChanges();

            return message;
        }

        public List<Message> GetAllUnread(int userId)
        {
            return _dbContext.Messages.Where(m => m.RecipientId == userId && !m.IsRead).ToList();
        }

        public Message Send(Message message)
        {
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
            return message;

        }
    }
}
