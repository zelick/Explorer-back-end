using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorAndAuthorPolicy")]
    [Route("api/administration/checkpointequests")]
    public class CheckpointRequestController : BaseApiController
    {
        private readonly ICheckpointRequestService _checkpointRequestService;

        public CheckpointRequestController(ICheckpointRequestService checkpointRequestService)
        {
            _checkpointRequestService = checkpointRequestService;
        }

        [HttpPost]
        public ActionResult<CheckpointRequestDto> Create([FromBody] CheckpointRequestDto request)
        {
            var result = _checkpointRequestService.Create(request);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<List<CheckpointRequestDto>> GetAll()
        {
            var result = _checkpointRequestService.GetAll();
            return CreateResponse(result);
        }

        [HttpPut("accept/{id:int}/{notificationComment:string}")]
        public ActionResult<ObjectRequestDto> AcceptRequest(int id, string notificationComment)
        {
            var result = _checkpointRequestService.AcceptRequest(id, notificationComment);
            return CreateResponse(result);
        }

        [HttpPut("reject/{id:int}/{notificationComment:string}")]
        public ActionResult<ObjectRequestDto> RejectRequest(int id, string notificationComment)
        {
            var result = _checkpointRequestService.RejectRequest(id, notificationComment);
            return CreateResponse(result);
        }
    }
}
