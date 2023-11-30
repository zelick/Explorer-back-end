using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorAndAuthorPolicy")]
    [Route("api/administration/encounterRequests")]
    public class EncounterRequestController : BaseApiController
    {
        private readonly IEncounterRequestService _encounterRequestService;

        public EncounterRequestController(IEncounterRequestService encounterRequestService)
        {
            _encounterRequestService = encounterRequestService;
        }

        [HttpPost]
        public ActionResult<EncounterRequestDto> Create([FromBody] EncounterRequestDto request)
        {
            var result = _encounterRequestService.Create(request);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<List<EncounterRequestDto>> GetAll()
        {
            var result = _encounterRequestService.GetPaged(0,0);
            return CreateResponse(result);
        }

        [HttpPut("accept/{id:int}")]
        public ActionResult<EncounterRequestDto> AcceptRequest(int id)
        {
            var result = _encounterRequestService.AcceptRequest(id);
            return CreateResponse(result);
        }

        [HttpPut("reject/{id:int}/{notificationComment}")]
        public ActionResult<EncounterRequestDto> RejectRequest(int id)
        {
            var result = _encounterRequestService.RejectRequest(id);
            return CreateResponse(result);
        }
    }
}
