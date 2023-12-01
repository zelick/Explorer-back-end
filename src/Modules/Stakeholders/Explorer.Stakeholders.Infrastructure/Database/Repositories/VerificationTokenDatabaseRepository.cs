using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class VerificationTokenDatabaseRepository : IVerificationTokenRepository
    {
        private readonly StakeholdersContext _dbContext;

        public VerificationTokenDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public VerificationToken CreateVerificationToken(long userId)
        {
            VerificationToken token = new VerificationToken(userId,DateTime.UtcNow, Guid.NewGuid().ToString());
            _dbContext.VerificationTokens.Add(token);
            _dbContext.SaveChanges();
            return token;
        }

        public VerificationToken GetByTokenData(string tokenData)
        {
            try
            {
                var token = _dbContext.VerificationTokens.FirstOrDefault(i => i.TokenData == tokenData);
                return token;
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        public VerificationToken GetByUserId(long userId)
        {
            try
            {
                var token = _dbContext.VerificationTokens.FirstOrDefault(i => i.UserId == userId);
                return token;
            }
            catch(Exception e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }
    }
}
