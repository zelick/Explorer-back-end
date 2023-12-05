using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Encounters
{
    [Route("api/tourist/encounter-execution")]
    [Authorize(Policy = "touristPolicy")]
    public class EncounterExecutionController : BaseApiController
    {
        private readonly IEncounterExecutionService _encounterExecutionService;
        private readonly IEncounterService _encounterService;

        public EncounterExecutionController(IEncounterExecutionService encounterExecutionService, IEncounterService encounterService)
        {
            _encounterExecutionService = encounterExecutionService;
            _encounterService = encounterService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<EncounterDto> GetById(int id)
        {
            var result = _encounterExecutionService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<EncounterExecutionDto> Update([FromForm] EncounterExecutionDto encounterExecution)
        {
            var result = _encounterExecutionService.Update(encounterExecution, User.PersonId());
            return CreateResponse(result);
        }

        [HttpPut("completed/{id:int}")]
        public ActionResult<EncounterExecutionDto> CompleteExecusion(int id)
        {
            var result = _encounterExecutionService.CompleteExecusion(id, User.PersonId());
            return CreateResponse(result);
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterExecutionService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpGet("get-all/{id:int}")]
        public ActionResult<PagedResult<EncounterExecutionDto>> GetAllByTourist(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            if (id != User.PersonId())
            {
                return Unauthorized();
            }
            var result = _encounterExecutionService.GetAllByTourist(id, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("get-all-completed")]
        public ActionResult<PagedResult<EncounterExecutionDto>> GetAllCompletedByTourist([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterExecutionService.GetAllCompletedByTourist(User.PersonId(), page, pageSize);
            return CreateResponse(result);
        }
        [HttpPut("activate/{id:int}")]
        public ActionResult<EncounterExecutionDto> Activate([FromRoute] int id, [FromForm] double touristLatitude, [FromForm] double touristLongitude)
        {
            var result = _encounterExecutionService.Activate(User.PersonId(), touristLatitude, touristLongitude, id);
            return CreateResponse(result);
        }

        [HttpGet("get-by-tour/{id:int}")]
        public ActionResult<EncounterExecutionDto> GetByTour([FromRoute] int id, [FromQuery] double touristLatitude, [FromQuery] double touristLongitude)
        {
            var result = _encounterExecutionService.GetVisibleByTour(id, touristLongitude, touristLatitude, User.PersonId());
            if(result.IsSuccess)
                result = _encounterService.AddEncounter(result.Value);
            return CreateResponse(result);
        }

        [HttpGet("social/checkRange/{id:int}/{tourId:int}")]
        public ActionResult<EncounterExecutionDto> CheckPosition([FromRoute] int tourId, [FromRoute] int id, [FromQuery] double touristLatitude, [FromQuery] double touristLongitude)
        {
            var result = _encounterExecutionService.GetWithUpdatedLocation(tourId, id, touristLongitude, touristLatitude, User.PersonId());
            if (result.IsSuccess)
                result = _encounterService.AddEncounter(result.Value);
            return CreateResponse(result);
        }

        [HttpGet("active/by-tour/{id:int}")]
        public ActionResult<List<EncounterExecutionDto>> GetActiveByTour([FromRoute] int id)
        {
            var result = _encounterExecutionService.GetActiveByTour(User.PersonId(), id);
            if (result.IsSuccess)
                result = _encounterService.AddEncounters(result.Value);
            return CreateResponse(result);
        }
    }
}
