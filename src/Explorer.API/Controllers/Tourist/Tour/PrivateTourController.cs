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
    }
}
