using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IReportedIssuesReviewService
    {
        Result<PagedResult<ReportedIssueDto>> GetPaged(int page, int pageSize);
    }
}
