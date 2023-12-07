using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class TouristDatabaseRepository : CrudDatabaseRepository<Tourist, StakeholdersContext>, ITouristRepository
    {
        private readonly StakeholdersContext _dbContext;
        public TouristDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Tourist GetTouristByUserId(long userId)
        {
            var tourist = _dbContext.Tourists
            .FirstOrDefault(t => t.Id == userId);

            return tourist;

            /*var tourist = _dbContext.Users
            .OfType<Tourist>()
            .FirstOrDefault(u => u.Id == userId);

            return tourist;*/
        }
    }
}
