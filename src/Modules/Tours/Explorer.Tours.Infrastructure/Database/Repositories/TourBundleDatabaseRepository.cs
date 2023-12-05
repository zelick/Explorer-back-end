using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class TourBundleDatabaseRepository : CrudDatabaseRepository<TourBundle, ToursContext>, ITourBundleRepository
{
    public TourBundleDatabaseRepository(ToursContext toursContext) : base(toursContext)
    {
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

    private static List<TourBundle> PageResults(int page, int pageSize, IQueryable<TourBundle> query)
    {
        if (pageSize != 0 && page != 0)
            return query.OrderByDescending(bp => bp.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        return query.ToList();
    }
}