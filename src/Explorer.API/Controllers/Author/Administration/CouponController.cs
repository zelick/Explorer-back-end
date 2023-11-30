using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/manipulation/coupon")]
    public class CouponController : BaseApiController
    {
        public CouponController() { }

        [HttpGet ("get-all")]
        public ActionResult<PagedResult<Object>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return CreateResponse(Result.Fail("Method is not implemented"));
        }

        [HttpPost("create")]
        public ActionResult<Object> Create([FromBody] Object coupon)
        {
            return CreateResponse(Result.Fail("Method is not implemented"));
        }

        [HttpPut("update/{id:int}")]
        public ActionResult<Object> Update([FromBody] Object equipment)
        {
            return CreateResponse(Result.Fail("Method is not implemented"));
        }

        [HttpDelete("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            return CreateResponse(Result.Fail("Method is not implemented"));
        }

    }
}
