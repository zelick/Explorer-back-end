using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories;


public class TourBundleDatabaseRepository : CrudDatabaseRepository<TourBundle, ToursContext>, ITourBundleRepository
{
	private readonly ToursContext _dbContext;
	public TourBundleDatabaseRepository(ToursContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

    public PagedResult<TourBundle> GetAllPublished(int page, int pageSize)
    {
        var query = DbContext.TourBundles
            .Include(tb => tb.Tours)
            .Where(bp => bp.Status == TourBundleStatus.Published);
        var count = query.Count();
        var items = PageResults(page, pageSize, query);
        return new PagedResult<TourBundle>(items, count);
    }

    public List<TourBundle> GetAllByAuthor(long id)
	{
		var authorBundles = _dbContext.TourBundles
			.Include(t => t.Tours).ThenInclude(c => c.Checkpoints)
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

	public TourBundle Update(TourBundle updatedBundle)
	{
		try
		{
			_dbContext.Attach(updatedBundle);
			_dbContext.Entry(updatedBundle).Collection(b => b.Tours).Load();
			foreach (var tour in updatedBundle.Tours.ToList())
			{
				_dbContext.Entry(tour).State = EntityState.Detached;
			}

			_dbContext.Attach(updatedBundle);
			_dbContext.Entry(updatedBundle).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}
		catch (DbUpdateException e)
		{
			throw new KeyNotFoundException(e.Message);
		}
		return updatedBundle;
	}

    private List<TourBundle> PageResults(int page, int pageSize, IQueryable<TourBundle> query)
    {
        if (pageSize != 0 && page != 0)
            return query.OrderByDescending(bp => bp.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        return query.ToList();
    }
}