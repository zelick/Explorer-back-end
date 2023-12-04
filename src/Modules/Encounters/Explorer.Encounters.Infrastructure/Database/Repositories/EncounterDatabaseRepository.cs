using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                _dbContext.Encounter.Add(encounter);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }

            return encounter;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _dbContext.Encounter.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Encounter Get(long id)
        {
            var encounter = _dbContext.Encounter.FirstOrDefault(e => e.Id == id);
            if (encounter == null) throw new KeyNotFoundException("Not found: " + id);

            return encounter;
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

        public Encounter Update(Encounter encounter)
        {
            try
            {
                _dbContext.Encounter.Update(encounter);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return encounter;
        }
    }
}
