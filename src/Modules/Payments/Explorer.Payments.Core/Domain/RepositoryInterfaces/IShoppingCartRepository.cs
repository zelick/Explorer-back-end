using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface IShoppingCartRepository : ICrudRepository<ShoppingCart>
{
    public bool ExistsByUser(long userId);
    public ShoppingCart GetByUser(long userId);
}