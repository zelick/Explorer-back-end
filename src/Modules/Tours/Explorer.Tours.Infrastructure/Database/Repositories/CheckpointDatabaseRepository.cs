using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class CheckpointDatabaseRepository : CrudDatabaseRepository<Checkpoint, ToursContext>, ICheckpointRepository
    {
        private readonly ToursContext _dbContext;
        public CheckpointDatabaseRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<Checkpoint> GetPagedByTour(int page, int pageSize, int id)
        {
            var query = DbContext.Checkpoints.Where(n => n.TourId == id);
            var count = query.Count();

            var pagedData = (page != 0 && pageSize != 0)
                ? query.Skip((page - 1) * pageSize).Take(pageSize)
                : query;

            var checkpoints = pagedData.ToList();
            return new PagedResult<Checkpoint>(checkpoints, count);
        }

        /*public Checkpoint SetPublicStatus(int id)
        {
            var checkpoint = _dbContext.Checkpoints.FirstOrDefault(n => n.Id == id);
            if (checkpoint == null) throw new KeyNotFoundException("Not found " + id);
            // checkpoint.SetPublicStatus();
            _dbContext.Checkpoints.Update(checkpoint);
            _dbContext.SaveChanges();
            return checkpoint;
        }*/
    }
}
