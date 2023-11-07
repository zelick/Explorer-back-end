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
    public class TourExecutionRepository : CrudDatabaseRepository<TourExecution, ToursContext>, ITourExecutionRepository
    {
        public TourExecutionRepository(ToursContext dbContext) : base(dbContext)
        { }
    }
}
