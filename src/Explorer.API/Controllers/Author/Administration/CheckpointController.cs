using Explorer.API.Services;
using Explorer.Blog.Core.Domain.BlogPosts;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/checkpoint")]
    public class CheckpointController : BaseApiController
    {
        private readonly ICheckpointService _checkpointService;
        private readonly ImageService _imageService;

        public CheckpointController(ICheckpointService checkpointService)
        {
            _checkpointService = checkpointService;
            _imageService = new ImageService();
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<List<CheckpointDto>> GetAllByTour([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            var result = _checkpointService.GetPagedByTour(page, pageSize, id);
            return CreateResponse(result);
        }

        [HttpGet("details/{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<CheckpointDto> GetById(int id)
        {
            var result = _checkpointService.Get(id);
            return CreateResponse(result);
        }

        /*
        [HttpPost]
        public ActionResult<CheckpointDto> Create([FromBody] CheckpointDto checkpoint, [FromQuery] int userId)
        {
            var result = _checkpointService.Create(checkpoint, userId);
            return CreateResponse(result);
        }
        */

        [HttpPut("{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<CheckpointDto> Update([FromBody] CheckpointDto checkpoint)
        {
            var result = _checkpointService.Update(checkpoint, User.PersonId());
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult Delete(int id)
        {
            var result = _checkpointService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpPut("createSecret/{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<CheckpointDto> CreateCheckpointSecret([FromBody] CheckpointSecretDto secretDto,int id)
        {
            var result = _checkpointService.CreateChechpointSecreat(secretDto,id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpPut("updateSecret/{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<CheckpointDto> UpdateCheckpointSecret([FromBody] CheckpointSecretDto secretDto, int id)
        {
            var result = _checkpointService.UpdateChechpointSecreat(secretDto, id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpPost("create/{status}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<CheckpointDto> Create([FromBody] CheckpointDto checkpoint, [FromRoute] string status)
        {
            var result = _checkpointService.Create(checkpoint, User.PersonId(), status);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<CheckpointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _checkpointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
