using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Tours.Core.Domain.Tours;
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
                .Include(t => t.Checkpoints)
                .Include(t=>t.TourRatings)
                .Where(t => t.AuthorId == id)
                .ToList();
        }

        public List<Tour> GetPublishedTours()
        {
            return _dbContext.Tours
                .Include(t => t.Equipment)
                .Include(t => t.Checkpoints)
                .Include(t => t.TourRatings)
                .Where(t => t.Status == TourStatus.Published)
                .ToList();
        }

        public Tour Get(long id)
        {
            var tour = _dbContext.Tours.Include(t => t.Equipment).Include(t => t.Checkpoints).Include(t=>t.TourRatings).FirstOrDefault(t => t.Id == id);
            if (tour == null) throw new KeyNotFoundException("Not found: " + id);

            return tour;
        }

        public PagedResult<Tour> GetPaged(int page, int pageSize)
        {
            var task = _dbContext.Tours
                .Include(t => t.Checkpoints)
                .Include(t => t.Equipment)
                .AsQueryable()
                .GetPagedById(page, pageSize);


            return task.Result;
        }

        public Tour Create(Tour tour)
        {
            _dbContext.Tours.Add(tour);
            _dbContext.SaveChanges();
            
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
            _dbContext.Tours.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Tour Close(long id)
        {
            var entity = Get(id);
            try
            {
                entity.Close();
                _dbContext.Tours.Update(entity);
                _dbContext.SaveChanges();
            }
            catch(DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            
            return entity;
        }

        public List<Tour> GetToursByIds(List<long> tourIds)
        {
            var result = _dbContext.Tours
                .Where(t => tourIds.Contains(t.Id))
                .ToList();

            return result;
        }

        public List<Tour> GetPublishedToursByAuthor(long authorId)
        {
            return _dbContext.Tours
                .Include(t => t.Equipment)
                .Include(t => t.Checkpoints)
                .Include(t => t.TourRatings)
                .Where(t => t.Status == TourStatus.Published && t.AuthorId == authorId)
                .ToList();
        }
    }
}
