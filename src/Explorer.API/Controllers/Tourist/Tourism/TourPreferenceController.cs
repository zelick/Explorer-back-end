using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tourism
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourism/preference")]
    public class TourPreferenceController : BaseApiController
    {
        private readonly ITourPreferenceService _tourPreferenceService;

        public TourPreferenceController(ITourPreferenceService tourPreferenceService)
        {
            _tourPreferenceService = tourPreferenceService;
        }

        [HttpPost]
        public ActionResult<TourPreferenceDto> Create([FromBody] TourPreferenceDto preference)
        {
            var result = _tourPreferenceService.Create(preference);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourPreferenceDto> Update([FromBody] TourPreferenceDto preference)
        {
            var result = _tourPreferenceService.Update(preference);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPreferenceService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<TourPreferenceDto>> GetPreferenceByCreator([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            var result = _tourPreferenceService.GetPreferenceByCreator(page, pageSize, id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<TourPreferenceDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPreferenceService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

    }

}
