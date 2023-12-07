using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ICouponRepository : ICrudRepository<Coupon>
{
    public List<Coupon> GetByUser(long Id);
    public Coupon GetByCode(string couponCode);
}
