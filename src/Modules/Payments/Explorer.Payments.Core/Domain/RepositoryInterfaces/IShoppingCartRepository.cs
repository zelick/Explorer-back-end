namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface IShoppingCartRepository
{
    ShoppingCart Get(long id);
    Coupon GetByCouponText(string couponText);
    public ShoppingCart GetByUser(long userId);
    ShoppingCart Create(ShoppingCart shoppingCart);
    ShoppingCart Update(ShoppingCart shoppingCart);
    void Delete(long id);
}