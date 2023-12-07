using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
	public class TourTourBundleDatabaseRepository : ITourTourBundleRepository
	{
		private readonly ToursContext _dbContext;
		public TourTourBundleDatabaseRepository(ToursContext dbContext)
		{
			_dbContext = dbContext;
		}

		public TourTourBundle AddTourToBundle(long tourBundleId, long tourId)
		{
			var ttb = new TourTourBundle(tourBundleId, tourId);
			_dbContext.TourTourBundles.Add(ttb);
			_dbContext.SaveChanges();
			return ttb;
		}

		public TourTourBundle RemoveTourFromBundle(long bundleId, long tourId)
		{
			var ttb = new TourTourBundle(bundleId, tourId);
			_dbContext.TourTourBundles.Remove(ttb);
			_dbContext.SaveChanges();
			return ttb;
		}

	}
}
