using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/reportingIssue")]
    public class ReportingIssueController : BaseApiController
    {
        private readonly IReportingIssueService _reportingIssueService;

        public ReportingIssueController(IReportingIssueService reportingIssueService)
        {
            _reportingIssueService = reportingIssueService;
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] ReportedIssueDto equipment)
        {
            if(equipment.Category.IsNullOrEmpty() || equipment.Priority==0 ||equipment.TourId==0 || equipment.TouristId == 0)
            {
                return BadRequest("Fill all the fields.");
            }
            var result = _reportingIssueService.Create(equipment);
            return CreateResponse(result);
        }
    }
}
