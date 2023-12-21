using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ITourPurchaseTokenRepository : ICrudRepository<TourPurchaseToken>
{
    public List<TourPurchaseToken> GetByUser(long userId);
    public bool ExistsByTourAndUser(long tourId, long userId);
    public bool HasPurchasedTour(long tourId, long userId);
}