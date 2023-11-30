using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterRequestDatabaseRepository : CrudDatabaseRepository<EncounterRequest, EncountersContext>, IEncounterRequestRepository
    {
        private readonly EncountersContext _dbContext;

        public EncounterRequestDatabaseRepository(EncountersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public EncounterRequest AcceptRequest(int id)
        {
            EncounterRequest requestToUpdate = _dbContext.EncounterRequests.FirstOrDefault(o => o.Id == id);
            if (requestToUpdate == null) throw new KeyNotFoundException("Not found " + id);
            requestToUpdate.AcceptRequest();
            _dbContext.SaveChanges();
            return requestToUpdate;
        }

        public EncounterRequest RejectRequest(int id)
        {
            EncounterRequest requestToUpdate = _dbContext.EncounterRequests.FirstOrDefault(o => o.Id == id);
            if (requestToUpdate == null) throw new KeyNotFoundException("Not found " + id);
            requestToUpdate.AcceptRequest();
            _dbContext.SaveChanges();
            return requestToUpdate;
        }
    }
}
