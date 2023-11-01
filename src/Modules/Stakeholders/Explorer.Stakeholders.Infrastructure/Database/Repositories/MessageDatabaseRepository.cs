using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class MessageDatabaseRepository : CrudDatabaseRepository<Message, StakeholdersContext>, IMessageRepository
    {
        private readonly StakeholdersContext _dbContext;

        public MessageDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
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
    }
}
