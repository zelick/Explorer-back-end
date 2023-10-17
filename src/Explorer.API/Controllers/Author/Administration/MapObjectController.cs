using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/mapObject")]
    public class MapObjectController : BaseApiController
    {
        private readonly IMapObjectService _mapObjectService;

        public MapObjectController(IMapObjectService mapObjectService)
        {
            _mapObjectService = mapObjectService;
        }

        [HttpGet]
        public ActionResult<PagedResult<MapObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _mapObjectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
