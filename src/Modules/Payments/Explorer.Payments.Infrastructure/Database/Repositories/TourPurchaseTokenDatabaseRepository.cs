using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class TourPurchaseTokenDatabaseRepository : CrudDatabaseRepository<TourPurchaseToken, PaymentsContext>, ITourPurchaseTokenRepository
{
    public TourPurchaseTokenDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }

    public List<TourPurchaseToken> GetByUser(long userId)
    {
        return DbContext.PurchaseTokens.Where(t => t.UserId == userId).ToList();
    }

    public bool ExistsByTourAndUser(long tourId, long userId)
    {
        return DbContext.PurchaseTokens.Any(t => t.TourId == tourId && t.UserId == userId);
    }

    public bool HasPurchasedTour(long tourId, long userId)
    {
        return DbContext.PurchaseTokens.Any(t => t.UserId == userId && t.TourId == tourId);
    }

    public int GetPurchasesNumberForTour(long tourId)
    {
        return DbContext.PurchaseTokens.Count(t => t.TourId == tourId);
    }
}