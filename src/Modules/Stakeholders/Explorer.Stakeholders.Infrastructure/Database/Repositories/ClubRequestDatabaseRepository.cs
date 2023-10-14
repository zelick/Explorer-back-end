using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubRequestDatabaseRepository : IClubRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubRequestDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ClubRequest Create(ClubRequest request)
        {
            _dbContext.Requests.Add(request);
            _dbContext.SaveChanges();
            return request;
        }

        public void Delete(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
