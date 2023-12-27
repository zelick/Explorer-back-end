using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Recommendation;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tour-execution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;
        private readonly ITourRecommendationService _tourRecommendationService;

        public TourExecutionController(ITourExecutionService tourExecutionService, ITourRecommendationService tourRecommendationService)
        {
            _tourExecutionService = tourExecutionService;
            _tourRecommendationService = tourRecommendationService;
        }
        [HttpPost("{tourId:int}")]
        public ActionResult<TourExecutionDto> Create(long tourId)
        {
            var result = _tourExecutionService.Create(User.PersonId(), tourId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourExecutionDto> CheckPosition([FromBody] TouristPositionDto touristPosition, long id)
        {
            var result = _tourExecutionService.CheckPosition(touristPosition, id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<TourExecutionDto> Get([FromQuery] long tourId)
        {
            var result = _tourExecutionService.GetInProgressByTourAndTourist(tourId, User.PersonId());
            return CreateResponse(result);
        }

        [HttpPut("abandoned")]
        public ActionResult<TourExecutionDto> Abandon([FromBody] long id)
        {
            var result = _tourExecutionService.Abandon(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpGet("get-suggested-tours/{id:int}")]
        public ActionResult<TourExecutionDto> GetSuggestedTours(long id)
        {
            var result = _tourExecutionService.GetSuggestedTours(id, User.PersonId(), _tourRecommendationService.GetAppropriateTours(User.PersonId()));
            return CreateResponse(result);
        }
    }
}
