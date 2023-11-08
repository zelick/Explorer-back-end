using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/checkpoint")]
    public class CheckpointController : BaseApiController
    {
        private readonly ICheckpointService _checkpointService;

        public CheckpointController(ICheckpointService checkpointService)
        {
            _checkpointService = checkpointService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CheckpointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _checkpointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<CheckpointDto>> GetAllByTour([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            var result = _checkpointService.GetPagedByTour(page, pageSize, id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CheckpointDto> Create([FromBody] CheckpointDto checkpoint)
        {
            var result = _checkpointService.Create(checkpoint);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CheckpointDto> Update([FromBody] CheckpointDto checkpoint)
        {
            var result = _checkpointService.Update(checkpoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _checkpointService.Delete(id);
            return CreateResponse(result);
        }


        [HttpPost("create")]
        public ActionResult<CheckpointDto> Create([FromBody] CheckpointDto checkpoint, [FromQuery] int userId, string status)
        {
            var result = _checkpointService.Create(checkpoint, userId, status);
            return CreateResponse(result);
        }
    }
}
