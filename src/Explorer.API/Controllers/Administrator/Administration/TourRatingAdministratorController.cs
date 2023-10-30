using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/tour-rating")]
    public class TourRatingAdministratorController : BaseApiController
    {
        private readonly ITourRatingService _tourRatingService;

        public TourRatingAdministratorController(ITourRatingService tourRatingService)
        {
            _tourRatingService = tourRatingService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourRatingDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourRatingService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourRatingService.Delete(id);
            return CreateResponse(result);
        }
    }
}
