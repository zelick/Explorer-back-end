using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class CompositeTourDatabaseRepository : ICompositeTourRepository
    {
        private readonly ToursContext _dbContext;

        public CompositeTour Create(CompositeTour entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public CompositeTour Get(long id)
        {
            throw new NotImplementedException();
        }

        public PagedResult<CompositeTour> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public CompositeTour Update(CompositeTour entity)
        {
            throw new NotImplementedException();
        }
    }
}
