using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class CheckpointRequestDatabaseRepository : CrudDatabaseRepository<CheckpointRequest, StakeholdersContext>, ICheckpointRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public CheckpointRequestDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public CheckpointRequest AcceptRequest(int id)
        {
            CheckpointRequest requestToUpdate = _dbContext.CheckpointRequests.FirstOrDefault(o => o.Id == id);
            if (requestToUpdate == null) throw new KeyNotFoundException("Not found " + id);
            requestToUpdate.AcceptRequest();
            return requestToUpdate;
        }

        public CheckpointRequest RejectRequest(int id)
        {
            CheckpointRequest requestToUpdate = _dbContext.CheckpointRequests.FirstOrDefault(o => o.Id == id);
            if (requestToUpdate == null) throw new KeyNotFoundException("Not found " + id);
            requestToUpdate.RejectRequest();
            return requestToUpdate;
        }
    }
}
