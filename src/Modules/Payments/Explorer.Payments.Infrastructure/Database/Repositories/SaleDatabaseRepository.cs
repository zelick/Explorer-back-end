using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class SaleDatabaseRepository : CrudDatabaseRepository<Sale, PaymentsContext>, ISaleRepository
{
    public SaleDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }

    public List<Sale> GetActiveSales()
    {
        var currentDateTime = DateTime.Now.ToUniversalTime();

        return DbContext.Sales
            .Where(s => s.Start <= currentDateTime && s.End >= currentDateTime)
            .ToList();
    }

    public List<Sale> GetActiveSalesForTour(long tourId)
    {
        var currentDateTime = DateTime.Now.ToUniversalTime();

        return DbContext.Sales
            .Where(s => s.ToursIds.Contains(tourId) && s.Start <= currentDateTime && s.End >= currentDateTime)
            .ToList();
    }
}