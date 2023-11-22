using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ReportedIssuesReviewService : CrudService<ReportedIssueDto, ReportedIssue>, IReportedIssuesReviewService
    {
        public ReportedIssuesReviewService(ICrudRepository<ReportedIssue> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
