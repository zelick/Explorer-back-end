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
    public class ObjectRequestDatabaseRepository : CrudDatabaseRepository<ObjectRequest, StakeholdersContext>, IObjectRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ObjectRequestDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public ObjectRequest AcceptRequest(int id)
        {
            ObjectRequest requestToUpdate = _dbContext.ObjectRequests.FirstOrDefault(o  => o.Id == id);
            if (requestToUpdate == null) throw new KeyNotFoundException("Not found " +  id);
            requestToUpdate.AcceptRequest();
            _dbContext.SaveChanges();
            return requestToUpdate;
        }

        public ObjectRequest RejectRequest(int id)
        {
            ObjectRequest requestToUpdate = _dbContext.ObjectRequests.FirstOrDefault(o => o.Id == id);
            if (requestToUpdate == null) throw new KeyNotFoundException("Not found " + id);
            requestToUpdate.RejectRequest();
            _dbContext.SaveChanges();
            return requestToUpdate;
        }
    }
}
