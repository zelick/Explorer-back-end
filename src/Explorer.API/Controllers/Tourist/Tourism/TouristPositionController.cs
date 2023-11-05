using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tourism
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourism/position")]
    public class TouristPositionController : BaseApiController
    {
        private readonly ITouristPositionService _touristPositionService;

        public TouristPositionController(ITouristPositionService touristPositionService)
        {
            _touristPositionService = touristPositionService;
        }

        [HttpPost]
        public ActionResult<TouristPositionDto> Create([FromBody] TouristPositionDto position)
        {
            var result = _touristPositionService.Create(position);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TouristPositionDto> Update([FromBody] TouristPositionDto position)
        {
            var result = _touristPositionService.Update(position);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _touristPositionService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<TouristPositionDto>> GetPreferenceByCreator([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            var result = _touristPositionService.GetPositionByCreator(page, pageSize, id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<TouristPositionDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _touristPositionService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

    }

}
