using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Dtos;

namespace Explorer.Payments.API.Public
{
    public interface ICouponService
    {
        Result<PagedResult<CouponDto>> GetPaged(int page, int pageSize);
        Result<CouponDto> Create(CreateCouponDto coupon);
        Result<CouponDto> Update(CouponDto coupon);
        Result Delete(int id);
    }
}
