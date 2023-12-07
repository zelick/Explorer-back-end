using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class HiddenLocationEncounterDatabaseRepository : IHiddenLocationEncounterRepository
    {
        private readonly EncountersContext _dbContext;

        public HiddenLocationEncounterDatabaseRepository(EncountersContext encountersContext)
        {
            _dbContext = encountersContext;
        }

        public HiddenLocationEncounter Create(HiddenLocationEncounter entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public HiddenLocationEncounter Get(long id)
        {
            var hiddenLocationEncounter = _dbContext.HiddenLocationEncounter.FirstOrDefault(e => e.Id == id);

            return hiddenLocationEncounter;
        }

        public PagedResult<HiddenLocationEncounter> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public HiddenLocationEncounter Update(HiddenLocationEncounter entity)
        {
            throw new NotImplementedException();
        }
    }
}
