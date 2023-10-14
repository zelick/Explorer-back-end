using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/request")]
    public class ClubRequestController: BaseApiController
    {
        private readonly IClubRequestService _clubRequestService;
        public ClubRequestController(IClubRequestService clubRequestService)
        {
            _clubRequestService = clubRequestService;
        }

        [HttpPost]
        public ClubRequestDto RegisterTourist([FromBody] ClubRequestDto request)
        {
            var result = _clubRequestService.RequestToJoinClub(request.ClubId, request.TouristId);
            //return CreateResponse(result); 
            return result;
        }

    }
}
