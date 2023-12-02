using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/privateTours")]
    public class PrivateTourController : BaseApiController
    {
        private readonly IPrivateTourService _tourService;

        public PrivateTourController(IPrivateTourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("{id:long}")]
        public ActionResult<List<PrivateTourDto>> GetPrivateTours(long id)
        {
            return CreateResponse(_tourService.GetAllByTourist(id));
        }

        [HttpPost("")]
        public ActionResult<List<PrivateTourDto>> CreatePrivateTour([FromBody] PrivateTourDto privateTourDto)
        {
            return CreateResponse(_tourService.Add(privateTourDto));
        }

        [HttpPut("start")]
        public ActionResult<List<PrivateTourDto>> StartPrivateTour([FromBody] PrivateTourDto privateTourDto)
        {
            return CreateResponse(_tourService.Start(privateTourDto));
        }

        [HttpPut("next-checkpoint")]
        public ActionResult<List<PrivateTourDto>> NextCheckpoint([FromBody] PrivateTourDto privateTourDto)
        {
            return CreateResponse(_tourService.Next(privateTourDto));
        }

        [HttpPut("finish")]
        public ActionResult<List<PrivateTourDto>> FinishPrivateTour([FromBody] PrivateTourDto privateTourDto)
        {
            return CreateResponse(_tourService.Finish(privateTourDto));
        }
    }
}
