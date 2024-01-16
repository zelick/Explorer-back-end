using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingSession;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class ShoppingCartDatabaseRepository : CrudDatabaseRepository<ShoppingCart, PaymentsContext>, IShoppingCartRepository
{
    public ShoppingCartDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }

    public ShoppingCart GetByUser(long userId)
    {
        var shoppingCart = DbContext.ShoppingCarts.FirstOrDefault(c => c.UserId == userId);
        if (shoppingCart == null) throw new KeyNotFoundException("Not found: " + userId);
        return shoppingCart;
    }

    public Coupon GetByCouponText(string couponText)
    {
        return DbContext.Coupons.FirstOrDefault(c => c.Code.Equals(couponText)); //Code=Text
    }
}