using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
	public class TourRatingDatabaseRepository : CrudDatabaseRepository<TourRating, ToursContext>, ITourRatingRepository
	{
		public TourRatingDatabaseRepository(ToursContext dbContext) : base(dbContext)
		{ }

		public TourRating GetTourRating(int ratingId)
		{
			return DbContext.TourRating.FirstOrDefault(tr => tr.Id == ratingId);
		}
	}
}
