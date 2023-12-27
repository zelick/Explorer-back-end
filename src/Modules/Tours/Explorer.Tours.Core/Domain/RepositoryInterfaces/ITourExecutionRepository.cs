using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourExecutionRepository:ICrudRepository<TourExecution>
    {
        public TourExecution GetExactExecution(long tourId, long touristId);
        TourExecution GetInProgressByTourAndTourist(long tourId, long touristId);
        List<TourExecution> GetCompletedByTour(long tourId);
        List<TourExecution> GetAllCompleted();
        List<TourExecution> GetActiveTourExecutions();
    }
}
