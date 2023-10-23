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

        [HttpPost]
        public ActionResult<MapObjectDto> Create([FromBody] MapObjectDto mapObject)
        {
            var result = _mapObjectService.Create(mapObject);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<MapObjectDto> Update([FromBody] MapObjectDto mapObject)
        {
            var result = _mapObjectService.Update(mapObject);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _mapObjectService.Delete(id);
            return CreateResponse(result);
        }
    }
}
