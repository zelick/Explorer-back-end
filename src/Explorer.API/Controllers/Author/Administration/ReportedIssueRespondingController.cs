using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/reported-issue-response")]
    public class ReportedIssueRespondingController : BaseApiController
    {
        private readonly IReportingIssueService _reportingIssueService;

        public ReportedIssueRespondingController(IReportingIssueService reportingIssueService)
        {
            _reportingIssueService = reportingIssueService;
        }

        [HttpPost("response/{id:int}")]
        public ActionResult<ReportedIssueDto> Respond([FromBody] ReportedIssueCommentDto ric, int id)
        {
            var result = _reportingIssueService.AddComment(id, ric);
            return CreateResponse(result);
        }
    }
}
