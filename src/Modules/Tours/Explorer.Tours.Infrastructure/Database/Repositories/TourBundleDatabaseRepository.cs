using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
	public class TourBundleDatabaseRepository : CrudDatabaseRepository<TourBundle, ToursContext>, ITourBundleRepository
	{
		private readonly ToursContext _dbContext;
		public TourBundleDatabaseRepository(ToursContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
	}
}
