using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
        [Authorize(Policy = "administratorPolicy")]
        [Route("api/administration/reportedIssues")]
        public class ReportedIssueAdministratorController : BaseApiController
        {
            private readonly IReportedIssueAdministratorService _reportedIssueAdministratorService;

            public ReportedIssueAdministratorController(IReportedIssueAdministratorService reportedIssueAdministratorService)
            {
                _reportedIssueAdministratorService = reportedIssueAdministratorService;
            }

            [HttpGet]
            public ActionResult<PagedResult<ReportedIssueDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
            {
                var result = _reportedIssueAdministratorService.GetPaged(page, pageSize);
                return CreateResponse(result);
            }
    }
}
