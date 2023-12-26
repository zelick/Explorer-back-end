using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class SecureTokenDatabaseRepository : ISecureTokenRepository
    {
        private readonly StakeholdersContext _dbContext;

        public SecureTokenDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SecureToken CreateSecureToken(long userId)
        {
            SecureToken token = new SecureToken(userId, Guid.NewGuid().ToString(), DateTime.UtcNow.AddHours(2));
            _dbContext.SecureTokens.Add(token);
            _dbContext.SaveChanges();
            return token;
        }

        public SecureToken GetByUserId(long userId)
        {
            try
            {
                var token = _dbContext.SecureTokens.FirstOrDefault(i => i.UserId == userId);
                return token;
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }
    }
}
