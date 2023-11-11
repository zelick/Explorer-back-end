using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "administratorAndAuthorPolicy")] //administratorAndAuthorPolicy authorPolicy
    [Route("api/administration/publicCheckpoint")]
    public class PublicCheckpointController : BaseApiController
    {
        private readonly IPublicCheckpointService _publicCheckpointService;

        public PublicCheckpointController(IPublicCheckpointService publicCheckpointService)
        {
            _publicCheckpointService = publicCheckpointService;
        }

        [HttpPost("create/{checkpointRequestId:int}/{notificationComment:string}")]
        public ActionResult<PublicCheckpointDto> Create(int checkpointRequestId, string notificationComment)
        {
            var result = _publicCheckpointService.Create(checkpointRequestId, notificationComment);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<PublicCheckpointDto> Update(PublicCheckpointDto publicCheckpointDto)
        {
            var result = _publicCheckpointService.Update(publicCheckpointDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _publicCheckpointService.Delete(id);
            return CreateResponse(result);
        }
    }
}
