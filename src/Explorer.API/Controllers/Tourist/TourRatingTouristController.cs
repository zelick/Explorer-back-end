using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-rating")]
    public class TourRatingTouristController : BaseApiController
    {
        private readonly ITourRatingService _tourRatingService;

        public TourRatingTouristController(ITourRatingService tourRatingService)
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
            if (tourRating.TourId == 0 || tourRating.TouristId == 0 || tourRating.Rating == 0 || tourRating.Rating > 5)
            {
                return BadRequest("Fill all the fields properly.");
            }
            var result = _tourRatingService.Create(tourRating);
            return CreateResponse(result);
        }
    }
}
