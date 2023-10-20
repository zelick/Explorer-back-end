using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class CheckpointDatabaseRepository : CrudDatabaseRepository<Checkpoint, ToursContext>, ICheckpointRepository
    {
        public CheckpointDatabaseRepository(ToursContext dbContext) : base(dbContext)
        {}
        public PagedResult<Checkpoint> GetPagedByTour(int page, int pageSize, int id)
        {
            var query = DbContext.Checkpoint.Where(n => n.TourID == id);
            var count = query.Count();

            var pagedData = (page != 0 && pageSize != 0)
                ? query.Skip((page - 1) * pageSize).Take(pageSize)
                : query;

            var checkpoints = pagedData.ToList();
            return new PagedResult<Checkpoint>(checkpoints, count);
        }
    }
}
