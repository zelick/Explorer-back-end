using Explorer.API.Services;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "administratorAndAuthorPolicy")]
    [Route("api/administration/mapObject")]
    public class MapObjectController : BaseApiController
    {
        private readonly IMapObjectService _mapObjectService;
        private readonly ImageService _imageService;

        public MapObjectController(IMapObjectService mapObjectService)
        {
            _mapObjectService = mapObjectService;
            _imageService = new ImageService();
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
        public ActionResult<MapObjectDto> Update([FromForm] MapObjectDto mapObject, IFormFile picture = null)
        {
            if (picture != null)
            {
                var pictureUrl = _imageService.UploadImages(new List<IFormFile> { picture });
                mapObject.PictureURL = pictureUrl[0];
            }
            var result = _mapObjectService.Update(mapObject);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _mapObjectService.DeleteObjectAndRequest(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<MapObjectDto> GetObject(int id)
        {
            var result = _mapObjectService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost("create/{userId:int}/{status}")]
        public ActionResult<MapObjectDto> Create([FromForm] MapObjectDto mapObject, [FromRoute] int userId, [FromRoute] string status, IFormFile picture = null)
        {
            if (picture != null)
            {
                var pictureUrl = _imageService.UploadImages(new List<IFormFile> { picture });
                mapObject.PictureURL = pictureUrl[0];
            }
            var result = _mapObjectService.Create(mapObject, userId, status);
            return CreateResponse(result);
        }
    }
}
