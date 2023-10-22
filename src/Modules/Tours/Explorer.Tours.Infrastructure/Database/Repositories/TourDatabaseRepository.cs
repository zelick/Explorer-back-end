using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourDatabaseRepository : ITourRepository
    {
        private readonly ToursContext _dbContext;

        public TourDatabaseRepository(ToursContext toursContext)
        {
            _dbContext = toursContext;
        }

        public bool Exists(long id)
        {
            return _dbContext.Tours.Any(t => t.Id == id);
        }

        public List<Tour> GetToursByAuthor(long id)
        {
            return _dbContext.Tours
                .Include(t => t.Equipment)
                .Where(t => t.AuthorId == id)
                .ToList();
        }

        public Tour Get(long id)
        {
            var tour = _dbContext.Tours.Include(t => t.Equipment).FirstOrDefault(t => t.Id == id);
            if (tour == null) throw new KeyNotFoundException("Not found: " + id);

            return tour;
        }

        public PagedResult<Tour> GetPaged(int page, int pageSize)
        {
            var task = _dbContext.Tours
                .Include(t => t.Equipment)
                .AsQueryable()
                .GetPagedById(page, pageSize);


            return task.Result;
        }

        public Tour Create(Tour tour)
        {
            try
            {
                _dbContext.Tours.Add(tour);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed to create the tour.");
            }
            return tour;
        }

        public Tour Update(Tour tour)
        {
            try
            {
                _dbContext.Tours.Update(tour);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return tour;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            try
            {
                _dbContext.Tours.Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                throw new KeyNotFoundException(e.Message);
            }
        }
    }
}
