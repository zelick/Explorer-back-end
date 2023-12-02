using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PrivateTourDatabaseRepository : CrudDatabaseRepository<PrivateTour, ToursContext>, IPrivateTourRepository
    {
        private readonly ToursContext _dbContext;
        public PrivateTourDatabaseRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Result<List<PrivateTour>> GetAllByTourist(long touristId)
        {
            var checkpoints = DbContext.PrivateTours.Where(n => n.ToursitId == touristId).ToList().ToResult();
            return checkpoints;
        }
    }
}
