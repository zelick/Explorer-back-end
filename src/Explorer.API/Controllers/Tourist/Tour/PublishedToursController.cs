using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shopping")]
    public class PublishedToursController : BaseApiController
    {
        private readonly ITourService _tourService;

        public PublishedToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<List<TourPreviewDto>> GetPublishedTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetFilteredPublishedTours(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("details/{id:int}")]
        public ActionResult<List<TourPreviewDto>> GetPublishedTour(int id)
        {
            var result = _tourService.GetPublishedTour(id);
            return CreateResponse(result);
        }

        [HttpGet("averageRating/{tourId:int}")]
        public double GetAverageRating(long tourId)
        {
            var result = _tourService.GetAverageRating(tourId);
            return result;
        }
    }
}
