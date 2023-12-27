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
                var tokens = _dbContext.SecureTokens.Where(i => i.UserId == userId).ToList();
                var token = tokens.First();

                foreach (var t in tokens)
                {
                    if(t.ExpiryTime > token.ExpiryTime)
                    {
                        token = t;
                    }
                }

                return token;
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        public long GetUserIdBySecureToken(string tokenData)
        {
            var token = _dbContext.SecureTokens.FirstOrDefault(t => t.TokenData.Equals(tokenData));
            if (token == null) throw new KeyNotFoundException("Not found token with data: " + tokenData);
            return token.UserId;
        }

        public SecureToken UseSecureToken(long tokenId)
        {
            var token = _dbContext.SecureTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) throw new KeyNotFoundException("Not found token with ID: " + tokenId);
            token.UseSecureToken();
            _dbContext.SaveChanges();
            return token;
        }
    }
}
