using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class ShoppingCartDatabaseRepository : CrudDatabaseRepository<ShoppingCart, PaymentsContext>, IShoppingCartRepository
{
    public ShoppingCartDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }

    public bool ExistsByUser(long userId)
    {
        return DbContext.ShoppingCarts.Any(sc => sc.UserId == userId);
    }

    public ShoppingCart GetByUser(long userId)
    {
        var shoppingCart = DbContext.ShoppingCarts.FirstOrDefault(c => c.UserId == userId);
        if (shoppingCart == null) throw new KeyNotFoundException("Not found: " + userId);
        return shoppingCart;
    }
}

