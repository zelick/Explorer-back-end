using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using System.Linq;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourEquipmentDatabaseRepository : ITourEquipmentRepository
    {
        private readonly ToursContext _dbContext;

        public TourEquipmentDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(int tourId, int equipmentId)
        {
            return _dbContext.TourEquipment.Any(te => te.TourId == tourId && te.EquipmentId == equipmentId);
        }

        public TourEquipment AddEquipment(int tourId, int equipmentId)
        {
            var tourEquipment = new TourEquipment(tourId, equipmentId);

            _dbContext.TourEquipment.Add(tourEquipment);
            _dbContext.SaveChanges();

            return tourEquipment;
        }

        public TourEquipment RemoveEquipment(int tourId, int equipmentId)
        {
            var tourEquipment = new TourEquipment(tourId, equipmentId);

            _dbContext.TourEquipment.Remove(tourEquipment);
            _dbContext.SaveChanges();

            return tourEquipment;
        }

        public bool IsEquipmentValid(int tourId, List<long> equipmentIds)
        {
            return _dbContext.TourEquipment.Any(te => te.TourId == tourId && equipmentIds.Contains(te.EquipmentId));
        }
    }
}
