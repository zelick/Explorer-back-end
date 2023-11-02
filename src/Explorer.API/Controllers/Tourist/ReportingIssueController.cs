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
        public ActionResult<ReportedIssueDto> Create([FromBody] ReportedIssueDto reportedIssue)
        {
            reportedIssue.Comments = new List<ReportedIssueCommentDto>();
            if(reportedIssue.Category.IsNullOrEmpty() || reportedIssue.Priority==0 ||reportedIssue.TourId==0 || reportedIssue.TouristId == 0)
            {
                return BadRequest("Fill all the fields.");
            }
            var result = _reportingIssueService.Create(reportedIssue);
            return CreateResponse(result);
        }
        [HttpPut("resolve/{id:int}")]
        public ActionResult<EquipmentDto> Resolve(int id)
        {
            var result = _reportingIssueService.Resolve(id);
            return CreateResponse(result);
        }
    }
}
