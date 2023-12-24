using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Recommendation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shopping")]
    public class PublishedToursController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly ITourRecommendationService _tourRecommendationService;

        public PublishedToursController(ITourService tourService, ITourRecommendationService tourRecommendationService)
        {
            _tourService = tourService;
            _tourRecommendationService = tourRecommendationService;
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

        [HttpGet("recommendations/{id:int}")]
        public ActionResult<List<TourPreviewDto>> GetToursByAuthor(int id)
        {
            var result = _tourRecommendationService.GetAppropriateTours(id);
            return CreateResponse(result);
        }
    }
}
