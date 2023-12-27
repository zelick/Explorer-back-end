using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
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
                .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
                .Include(t => t.CompletedCheckpoints).FirstOrDefault(t => t.Id == id);
            if (tour == null) throw new KeyNotFoundException("Not found: " + id);

            return tour;
        }

        public PagedResult<TourExecution> GetPaged(int page, int pageSize)
        {
            var task = _dbContext.TourExecution
               .Include(t => t.CompletedCheckpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Equipment)
               .Include(t => t.Tour).ThenInclude(c => c.TourRatings)
               .AsQueryable()
               .GetPagedById(page, pageSize);


            return task.Result;
        }

        public List<TourExecution> GetActiveTourExecutions()
        {
            return _dbContext.TourExecution
               .Include(t => t.CompletedCheckpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Equipment)
               .Include(t => t.Tour).ThenInclude(c => c.TourRatings)
               .Where(t => t.ExecutionStatus == ExecutionStatus.InProgress).ToList();


        }
        /*.
        public TourExecution GetExactExecution(long tourId, long touristId)
        {
            var exactTourExecution = _dbContext.TourExecution
               .Include(t => t.CompletedCheckpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
               .FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);

            return exactTourExecution;

        }
        */

        public TourExecution GetExactExecution(long tourId, long touristId)
        {
            var exactTourExecution = _dbContext.TourExecution
               .Include(t => t.CompletedCheckpoints)
               .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
               .Where(t => t.TourId == tourId && t.TouristId == touristId).FirstOrDefault<TourExecution>();

            return exactTourExecution;

        }

        public TourExecution GetInProgressByTourAndTourist(long tourId, long touristId)
        {
            var tour = _dbContext.TourExecution
                 .Include(t => t.CompletedCheckpoints)
                 .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
                 .Include(t => t.Tour).ThenInclude(c => c.Equipment)
                 .Include(t => t.Tour).ThenInclude(c => c.TourRatings)
                 .Where(t => t.TourId == tourId && t.TouristId == touristId && t.ExecutionStatus == ExecutionStatus.InProgress).FirstOrDefault<TourExecution>();

            return tour;
        }

        public List<TourExecution> GetCompletedByTour(long tourId)
        {
            var executions = _dbContext.TourExecution
                .Include(t => t.CompletedCheckpoints)
                .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
                .Include(t => t.Tour).ThenInclude(c => c.Equipment)
                .Include(t => t.Tour).ThenInclude(c => c.TourRatings)
                .Where(t => t.TourId == tourId && t.ExecutionStatus == ExecutionStatus.Completed).ToList();

            return executions;

        }

        public List<TourExecution> GetAllCompleted()
        {
            return _dbContext.TourExecution
                .Include(t => t.CompletedCheckpoints)
                .Include(t => t.Tour).ThenInclude(c => c.Checkpoints)
                .Include(t => t.Tour).ThenInclude(c => c.Equipment)
                .Include(t => t.Tour).ThenInclude(c => c.TourRatings)
                .Where(t => t.ExecutionStatus == ExecutionStatus.Completed).ToList();
        }
    }
}
