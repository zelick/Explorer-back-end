using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourExecutionDatabaseRepository : CrudDatabaseRepository<TourExecution, ToursContext>, ITourExecutionRepository
    {
        private readonly ToursContext _dbContext;
        public TourExecutionDatabaseRepository(ToursContext toursContext): base(toursContext)
        {
            _dbContext = toursContext;
        }
        public TourExecution Get(long id)
        {
            var tour = _dbContext.TourExecution
                .Include(t => t.Tour)
                .Include(t => t.CompletedCheckpoints).FirstOrDefault(t => t.Id == id);
            if (tour == null) throw new KeyNotFoundException("Not found: " + id);

            return tour;
        }

        public PagedResult<TourExecution> GetPaged(int page, int pageSize)
        {
            var task = _dbContext.TourExecution
               .Include(t => t.CompletedCheckpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
               .AsQueryable()
               .GetPagedById(page, pageSize);


            return task.Result;
        }

        public TourExecution GetExactExecution(long tourId, long touristId)
        {
            var exactTourExecution = _dbContext.TourExecution
               .Include(t => t.CompletedCheckpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
               .FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);

            return exactTourExecution;

        }
    }
}
