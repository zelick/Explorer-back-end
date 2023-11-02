using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class ReportingIssueService : CrudService<ReportedIssueDto, ReportedIssue>, IReportingIssueService
    {
        private readonly IReportedIssueRepository _reportedIssuesRepository;
        public ReportingIssueService(ICrudRepository<ReportedIssue> repository, IMapper mapper, IReportedIssueRepository issuerepo) : base(repository, mapper) {
            _reportedIssuesRepository = issuerepo;
        }
        public Result<ReportedIssueDto> Resolve(long id)
        {
            try
            {
                var result = _reportedIssuesRepository.Resolve(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        public Result<ReportedIssueDto> AddComment(long id, ReportedIssueCommentDto reportedISsueComment)
        {
            try
            {
                var result = _reportedIssuesRepository.AddComment(id, new ReportedIssueComment(reportedISsueComment.Text, DateTime.Now, reportedISsueComment.CreatorId));
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
