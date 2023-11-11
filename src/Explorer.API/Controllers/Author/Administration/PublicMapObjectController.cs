using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "administratorAndAuthorPolicy")]
    [Route("api/administration/publicMapObject")]
    public class PublicMapObjectController : BaseApiController
    {
        private readonly IPublicObjectService _publicObjectService;

        public PublicMapObjectController(IPublicObjectService publicObjectService)
        {
            _publicObjectService = publicObjectService;
        }

        [HttpPost("create/{objectRequestId:int}/{notificationComment}")]
        public ActionResult<PublicMapObjectDto> Create(int objectRequestId, string notificationComment)
        {
            var result = _publicObjectService.Create(objectRequestId, notificationComment);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<PublicCheckpointDto> Update(PublicMapObjectDto publicMapObjectDto)
        {
            var result = _publicObjectService.Update(publicMapObjectDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _publicObjectService.Delete(id);
            return CreateResponse(result);
        }
    }
}
