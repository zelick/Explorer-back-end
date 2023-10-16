using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/administration/tourRating")]
    public class TourRatingController : BaseApiController
    {
        private readonly ITourRatingService _tourRatingService;

        public TourRatingController(ITourRatingService tourRatingService)
        {
            _tourRatingService = tourRatingService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourRatingDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourRatingService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourRatingDto> Create([FromBody] TourRatingDto tourRating)
        {
            var result = _tourRatingService.Create(tourRating);
            return CreateResponse(result);
        }
    }
}
