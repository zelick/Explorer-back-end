using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IReportingIssueService
    {
        Result<ReportedIssueDto> Create(ReportedIssueDto reportedIssue);
        Result<ReportedIssueDto> Resolve(long id);
        Result<ReportedIssueDto> AddComment(long id, ReportedIssueCommentDto reportedIssueComment);
        Result<ReportedIssueDto> AddDeadline(int id, DateTime deadline);
        Result<ReportedIssueDto> PenalizeAthor(int id);
        Result<ReportedIssueDto> Close(int id);
        Result<PagedResult<ReportedIssueDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<ReportedIssueDto>> GetPagedByAuthor(long id, int page, int pageSize);
        Result<PagedResult<ReportedIssueDto>> GetPagedByTourist(long id, int page, int pageSize);
    }
}
