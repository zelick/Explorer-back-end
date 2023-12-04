using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterDatabaseRepository:IEncounterRepository
    {
        private readonly EncountersContext _dbContext;

        public EncounterDatabaseRepository(EncountersContext encountersContext)
        {
            _dbContext = encountersContext;
        }

        public Encounter Create(Encounter encounter)
        {
            _dbContext.Encounter.Add(encounter);
            _dbContext.SaveChanges();

            return encounter;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Encounter Get(long id)
        {
            throw new NotImplementedException();
        }

        public PagedResult<Encounter> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Encounter MakeEncounterPublished(long id)
        {
            Encounter encounterToUpdate = _dbContext.Encounter.FirstOrDefault(o => o.Id == id);
            if (encounterToUpdate == null) throw new KeyNotFoundException("Not found " + id);
            encounterToUpdate.MakeEncounterPublished();
            _dbContext.SaveChanges();
            return encounterToUpdate;
        }

        public Encounter Update(Encounter entity)
        {
            throw new NotImplementedException();
        }
    }
}
