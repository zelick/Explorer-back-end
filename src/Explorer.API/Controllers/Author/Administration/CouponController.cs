using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    //[Authorize(Policy = "authorPolicy")]
    [Route("api/manipulation/coupon")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService service)
        {
            _couponService = service;
        }

        [HttpGet ("get-all")]
        public ActionResult<PagedResult<CouponDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _couponService.GetPaged(page, pageSize);

            return CreateResponse(result);
        }

        [HttpPost("create")]
        public ActionResult<CouponDto> Create([FromBody] CreateCouponDto coupon)
        {
            coupon.SellerId = User.PersonId();
            var result = _couponService.Create(coupon);

            return CreateResponse(result);
        }

        [HttpPut("update/{id:int}")]
        public ActionResult<CouponDto> Update([FromBody] CouponDto coupon)
        {
            var result = _couponService.Update(coupon);

            return CreateResponse(result);
        }

        [HttpDelete("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _couponService.Delete(id);
            return CreateResponse(result);
        }

    }
}
