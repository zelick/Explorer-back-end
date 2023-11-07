using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tour-execution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService)
        {
            _tourExecutionService = tourExecutionService;
        }
        [HttpPost]
        public ActionResult<TourExecutionDto> Create([FromBody] TourExecutionDto tourExecution)
        {
            var result = _tourExecutionService.Create(tourExecution);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourExecutionDto> CheckPosition([FromBody] TouristPositionDto touristPosition, long id)
        {
            var result = _tourExecutionService.CheckPosition(touristPosition, id);
            return CreateResponse(result);
        }
    }
}
