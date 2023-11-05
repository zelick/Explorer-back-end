using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
        [Authorize(Policy = "administratorPolicy")]
        [Route("api/administration/reportedIssues")]
        public class ReportedIssuesReviewController : BaseApiController
        {
            private readonly IReportingIssueService _reportedIssueAdministratorService;

            public ReportedIssuesReviewController(IReportingIssueService reportedIssueAdministratorService)
            {
                _reportedIssueAdministratorService = reportedIssueAdministratorService;
            }

            [HttpGet]
            public ActionResult<PagedResult<ReportedIssueDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
            {
                var result = _reportedIssueAdministratorService.GetPaged(page, pageSize);
                return CreateResponse(result);
            }

            [HttpPost("comment/{id:int}")]
            public ActionResult<ReportedIssueDto> Respond([FromRoute] int id, [FromBody] ReportedIssueCommentDto ric)
            {
                var result = _reportedIssueAdministratorService.AddComment(id, ric);
                return CreateResponse(result);
            }

            [HttpPut("deadline/{id:int}")]
            public ActionResult<ReportedIssueDto> AddDeadline(int id, [FromBody] DateTime deadline)
            {
                var result = _reportedIssueAdministratorService.AddDeadline(id, deadline);
                return CreateResponse(result);
            }

            [HttpPut("penalizeAuthor/{id:int}")]
            public ActionResult<ReportedIssueDto> PenalizeAuthor(int id)
            {
                var result = _reportedIssueAdministratorService.PenalizeAthor(id);
                return CreateResponse(result);
            }

            [HttpPut("closeReportedIssue/{id:int}")]
            public ActionResult<ReportedIssueDto> Close(int id)
            {
                var result = _reportedIssueAdministratorService.Close(id);
                return CreateResponse(result);
            }
    }
}
