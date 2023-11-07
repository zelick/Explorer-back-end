using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/publicCheckpoint")]
    public class PublicCheckpointController : BaseApiController
    {
        private readonly IPublicCheckpointService _publicCheckpointService;

        public PublicCheckpointController(IPublicCheckpointService publicCheckpointService)
        {
            _publicCheckpointService = publicCheckpointService;
        }

        [HttpPost("create")]
        public ActionResult<PublicCheckpointDto> Create([FromQuery] int checkpointRequestId)
        {
            var result = _publicCheckpointService.Create(checkpointRequestId);
            return CreateResponse(result);
        }
    }
}
