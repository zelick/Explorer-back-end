using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterExecutionDatabaseRepository : IEncounterExecutionRepository
    {
        private readonly EncountersContext _dbContext;

        public EncounterExecutionDatabaseRepository(EncountersContext encountersContext)
        {
            _dbContext = encountersContext;
        }
        
        public PagedResult<EncounterExecution> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
        public EncounterExecution Create(EncounterExecution encounterExecution)
        {
            try
            {
                _dbContext.EncounterExecution.Add(encounterExecution);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }

            return encounterExecution;
        }
        public EncounterExecution Get(long id)
        {
            var encounterEecution = _dbContext.EncounterExecution.Include(e => e.Encounter).FirstOrDefault(e => e.Id == id);
            if (encounterEecution == null) throw new KeyNotFoundException("Not found: " + id);

            return encounterEecution;
        }
        public EncounterExecution Update(EncounterExecution encounterExecution)
        {
            try
            {
                _dbContext.EncounterExecution.Update(encounterExecution);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return encounterExecution;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _dbContext.EncounterExecution.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<EncounterExecution> GetAllByTourist(long touristId)
        {
            return _dbContext.EncounterExecution
                .Include(e => e.Encounter)
                .Where(e => (e.TouristId == touristId))
                .ToList();
        }
        public List<EncounterExecution> GetAllCompletedByTourist(long touristId)
        {
            return _dbContext.EncounterExecution
                .Include(e => e.Encounter)
                .Where(e => (e.TouristId == touristId) && (e.Status == EncounterExecutionStatus.Completed))
                .ToList();
        }

        public EncounterExecution FindByEncounterId(long encounterId)
        {
            var encounterExecution = _dbContext.EncounterExecution
            .Include(e => e.Encounter)
            .FirstOrDefault(e => e.Encounter.Id == encounterId);

            return encounterExecution;
        }

        public EncounterExecution GetByEncounterAndTourist(long touristId, long encounterId)
        {
            return _dbContext.EncounterExecution
                .Include(e => e.Encounter)
                .Where(e => (e.TouristId == touristId) && (e.EncounterId == encounterId)).FirstOrDefault();
        }

        public List<EncounterExecution> GetActiveByTourist(long touristId)
        {
            return _dbContext.EncounterExecution
                .Include(e => e.Encounter)
                .Where(e => (e.TouristId == touristId) && e.Status.Equals(EncounterExecutionStatus.Active))
                .ToList();
        }

        public List<EncounterExecution> UpdateRange(List<EncounterExecution> encounters)
        {
            try
            {
                _dbContext.EncounterExecution.UpdateRange(encounters);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return encounters;
        }

        public List<EncounterExecution> GetBySocialEncounter(long socialEncounterId)
        {
            return _dbContext.EncounterExecution
                .Include(e => e.Encounter)
                .Where(e => (e.EncounterId == socialEncounterId) && e.Encounter.Type == EncounterType.Social)
                .ToList();
        }

        public List<EncounterExecution> GetByLocationEncounter(long locationEncounterId)
        {
            return _dbContext.EncounterExecution
                .Include(e => e.Encounter)
                .Where(e => (e.EncounterId == locationEncounterId) && e.Encounter.Type == EncounterType.Location)
                .ToList();
        }
    }
}
