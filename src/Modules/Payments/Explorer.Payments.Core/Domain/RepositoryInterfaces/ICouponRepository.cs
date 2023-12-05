using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ICouponRepository : ICrudRepository<Coupon>
{
    public Coupon GetByCode(string couponCode);
}
