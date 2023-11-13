using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class EquipmentDatabaseRepository : IEquipmentRepository
    {
        private readonly ToursContext _dbContext;

        public EquipmentDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Equipment Create(Equipment equipment)
        {
            _dbContext.Equipment.Add(equipment);
            _dbContext.SaveChanges();
            return equipment;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _dbContext.Equipment.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _dbContext.Equipment.Any(e => e.Id == id);
        }

        public Equipment Get(long id)
        {
            var equipment = _dbContext.Equipment.Find(id);
            if (equipment == null) throw new KeyNotFoundException("Not found: " + id);
            return equipment;
        }

        public List<Equipment> GetAvailable(List<long> currentEquipmentIds)
        {
            var availableEquipment = _dbContext.Equipment.Where(e => !currentEquipmentIds.Contains(e.Id)).ToList();
            return availableEquipment;
        }

        public PagedResult<Equipment> GetPaged(int page, int pageSize)
        {
            var task = _dbContext.Equipment.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Equipment Update(Equipment equipment)
        {
            try
            {
                _dbContext.Equipment.Update(equipment);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return equipment;
        }
    }
}
