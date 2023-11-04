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
        private readonly IReportedIssueNotificationRepository _reportedIssueNotificationRepository;
        private readonly ITourRepository _tourRepository;
        public ReportingIssueService(ICrudRepository<ReportedIssue> repository, IMapper mapper,
                                     IReportedIssueRepository issuerepo, 
                                     IReportedIssueNotificationRepository reportedIssueNotificationRepository,
                                     ITourRepository tourRepository) : base(repository, mapper)
        {
            _reportedIssuesRepository = issuerepo;
            _reportedIssueNotificationRepository = reportedIssueNotificationRepository;
            _tourRepository = tourRepository;
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
                GenerateNotification(result, reportedISsueComment.CreatorId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<ReportedIssueDto> AddDeadline(int id, DateTime deadline)
        {
            try
            {
                var issue = _reportedIssuesRepository.Get(id);
                var result = _reportedIssuesRepository.AddDeadline(id, deadline);
                _reportedIssueNotificationRepository.Create(issue.Tour.AuthorId, id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<ReportedIssueDto> PenalizeAthor(int id)
        {
            try
            {
                var issue = _reportedIssuesRepository.Get(id);
                var result = _tourRepository.Close(issue.Tour.Id);
                return MapToDto(issue);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<ReportedIssueDto> Close(int id)
        {
            try
            {
                var result = _reportedIssuesRepository.Close(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        private void GenerateNotification(ReportedIssue reportedIssue, int commentCreatorId)
        {
            // generate notification for new comment
            if (commentCreatorId == reportedIssue.TouristId)
            {
                var notifTourist = _reportedIssueNotificationRepository.Create(reportedIssue.Tour.AuthorId, reportedIssue.Id);
            }
            else
            {
                var notifAuthor = _reportedIssueNotificationRepository.Create(reportedIssue.TouristId, reportedIssue.Id);
            }
        }

        public Result<PagedResult<ReportedIssueDto>> GetPaged(int page, int pageSize)
        {
            try
            {
                var result = _reportedIssuesRepository.GetPaged(page, pageSize);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        public Result<PagedResult<ReportedIssueDto>> GetPagedByAuthor(long id, int page, int pageSize)
        {
            try
            {
                var result = _reportedIssuesRepository.GetPagedByAuthor(id, page, pageSize);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        public Result<PagedResult<ReportedIssueDto>> GetPagedByTourist(long id, int page, int pageSize)
        {
            try
            {
                var result = _reportedIssuesRepository.GetPagedByTourist(id, page, pageSize);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        
    }
}
