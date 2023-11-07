using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/publicMapObject")]
    public class PublicMapObjectController : BaseApiController
    {
        private readonly IPublicObjectService _publicObjectService;

        public PublicMapObjectController(IPublicObjectService publicObjectService)
        {
            _publicObjectService = publicObjectService;
        }

        [HttpPost("create")]
        public ActionResult<PublicMapObjectDto> Create([FromQuery] int objectRequestId)
        {
            var result = _publicObjectService.Create(objectRequestId);
            return CreateResponse(result);
        }
    }
}
