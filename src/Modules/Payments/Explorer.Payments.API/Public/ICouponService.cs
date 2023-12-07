using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface ICouponService
    {
        Result<PagedResult<CouponDto>> GetPaged(int page, int pageSize);
        Result<List<CouponDto>> GetByUser(long Id);
        Result<CouponDto> Create(CreateCouponDto coupon);
        Result<CouponDto> Update(CouponDto coupon);
        Result Delete(int id);
    }
}
