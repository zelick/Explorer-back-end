using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class SocialEncounterDatabaseRepository : ISocialEncounterRepository
    {
        private readonly EncountersContext _dbContext;

        public SocialEncounterDatabaseRepository(EncountersContext encountersContext)
        {
            _dbContext = encountersContext;
        }

        public SocialEncounter Create(SocialEncounter entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public SocialEncounter Get(long id)
        {
            var socialEncounter = _dbContext.SocialEncounter.FirstOrDefault(e => e.Id == id);

            return socialEncounter;
        }

        public PagedResult<SocialEncounter> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public SocialEncounter Update(SocialEncounter entity)
        {
            throw new NotImplementedException();
        }
    }
}
