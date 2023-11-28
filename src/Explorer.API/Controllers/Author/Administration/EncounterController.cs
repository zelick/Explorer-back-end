using Explorer.API.Services;
using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/encounter")]

    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }
        

        [HttpPost]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<EncounterDto> Create([FromForm] EncounterDto encounter,[FromQuery] long checkpointId, [FromForm] List<IFormFile>? image = null)
        {
            if(User.PersonId()!=encounter.AuthorId) { return CreateResponse(Result.Fail(FailureCode.Forbidden)); }
            throw new NotImplementedException();
        }
    }
}
