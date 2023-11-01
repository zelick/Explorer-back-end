using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class UserFollowerDatabaseRepository : IUserFollowerRepository
    {
        private readonly StakeholdersContext _dbContext;
        public UserFollowerDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserFollower AddFollower(int userId, int followerId)
        {
            var userFollower = new UserFollower(userId, followerId);
            _dbContext.UserFollower.Add(userFollower);
            _dbContext.SaveChanges();
            
            return userFollower;
        }

    }
}
