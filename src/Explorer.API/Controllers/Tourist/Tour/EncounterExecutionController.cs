using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Route("api/administration/encounterExecition")]
    [Authorize(Policy = "touristPolicy")]
    public class EncounterExecutionController : BaseApiController
    {
        private readonly IEncounterService _encounterService;

        public EncounterExecutionController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpPut("{id:int}")]
        public ActionResult<EncounterDto> Activate([FromRoute] int id, [FromQuery] double touristLongitude, [FromQuery] double touristLatitude)
        {
            var result = _encounterService.Activate(id, touristLongitude, touristLatitude, User.PersonId());
            return CreateResponse(result);
        }

        [HttpPut("checkPosition/{id:int}")]
        public ActionResult<EncounterDto> CheckIfInRange([FromRoute] int id, [FromQuery] double touristLongitude, [FromQuery] double touristLatitude)
        {
            var result = _encounterService.CheckIfInRange(id, touristLongitude, touristLatitude, User.PersonId());
            return CreateResponse(result);
        }
    }
}
