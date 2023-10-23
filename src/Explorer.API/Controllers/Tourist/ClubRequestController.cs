using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/request")]
    public class ClubRequestController : BaseApiController
    {
        private readonly IClubRequestService _clubRequestService;
        public ClubRequestController(IClubRequestService clubRequestService)
        {
            _clubRequestService = clubRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubRequestDto> Create([FromBody] ClubRequestDto request)
        {
            var result = _clubRequestService.Create(request);
            return CreateResponse(result);
        }


        [HttpPut("{id:int}")]
        public ActionResult<ClubRequestDto> Update([FromBody] ClubRequestDto request)
        {

            var result = _clubRequestService.Update(request);
            return CreateResponse(result);

        }

        /*
                [HttpPut("{id:int}/approve")]
                public ActionResult<ClubRequestDto> UpdateIfApproved([FromBody] ClubRequestDto request)
                {
                    // request.Status = ClubRequestStatus.Accepted;

                    if (Enum.TryParse(request.Status, out ClubRequestStatus status))
                    {
                       // request.Status = status;

                        // Pozovi servis za ažuriranje
                        var result = _clubRequestService.Update(request);
                        return CreateResponse(result);
                    }
                    else
                    {
                        return BadRequest("Invalid ClubRequestStatus value");
                    }
                }

                [HttpPut("{id:int}/reject")]
                public ActionResult<ClubRequestDto> UpdateIfRejected([FromBody] ClubRequestDto request)
                {
                    //promeni status?
                    var result = _clubRequestService.Update(request);
                    return CreateResponse(result);
                }*/

        [HttpDelete("deleteRequest/{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubRequestService.Delete(id);
            return CreateResponse(result);
        }

    }
}
