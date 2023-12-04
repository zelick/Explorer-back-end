using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
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

		public List<TourBundle> GetAllByAuthor(long id)
		{
			var authorBundles = _dbContext.TourBundles
				.Include(t => t.Tours)
				.Where(t => t.AuthorId == id)
				.ToList();
			foreach(TourBundle tb in authorBundles)
			{
				GetBundleWithTours(tb.Id);
			}
			return authorBundles;
		}

		public TourBundle GetBundleWithTours(long id)
		{
			var tourBundle = DbContext.TourBundles
			.Where(tb => tb.Id == id)
			.FirstOrDefault();

			if (tourBundle != null)
			{
				var ttbIds = DbContext.TourTourBundles
					.Where(ttb => ttb.TourBundleId == tourBundle.Id)
					.Select(ttb => ttb.TourId)
					.ToList();

				var tours = DbContext.Tours
					.Where(t => ttbIds.Contains(t.Id))
					.ToList();

				tourBundle.Tours = tours;
			}

			return tourBundle;
		}
	}
}
