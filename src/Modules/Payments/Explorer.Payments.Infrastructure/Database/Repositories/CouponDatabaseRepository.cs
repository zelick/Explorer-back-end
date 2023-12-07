using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class CouponDatabaseRepository : CrudDatabaseRepository<Coupon, PaymentsContext>, ICouponRepository
{
    public CouponDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }

    public Coupon GetByCode(string couponCode)
    {
        var coupon = DbContext.Coupons.FirstOrDefault(c => c.Code.Equals(couponCode));
        if (coupon == null) throw new KeyNotFoundException("Not found: " + couponCode);
        return coupon;
    }

    public List<Coupon> GetByUser(long Id)
    {
        var coupons = DbContext.Coupons.Where(c => c.SellerId == Id).ToList();
        return coupons;
    }
}