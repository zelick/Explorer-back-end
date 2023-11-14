using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ReportedIssueNotificationService : CrudService<ReportedIssueNotificationDto, ReportedIssueNotification>, IReportedIssueNotificationService
    {
        private readonly IReportedIssueNotificationRepository _reportedIssueNotificationsRepository;
        public ReportedIssueNotificationService(ICrudRepository<ReportedIssueNotification> repository, IMapper mapper, IReportedIssueNotificationRepository reportedIssueNotificationRepository, IReportedIssueRepository reportedIssueRepository) : base(repository, mapper)
        {
            _reportedIssueNotificationsRepository = reportedIssueNotificationRepository;
        }
        public Result<ReportedIssueNotificationDto> Create(string description, long userId, long reportedIssueId)
        {
            try
            {
                var result = _reportedIssueNotificationsRepository.Create(description, userId, reportedIssueId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<ReportedIssueNotificationDto>> GetAllByUser(long id, int page, int pageSize)
        {
            try
            {
                var result = _reportedIssueNotificationsRepository.GetAllByUser(id);
                var paged = new PagedResult<ReportedIssueNotification>(result, result.Count());
                return MapToDto(paged);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<ReportedIssueNotificationDto>> GetUnreadByUser(long id, int page, int pageSize)
        {
            try
            {
                var result = _reportedIssueNotificationsRepository.GetUnreadByUser(id);
                var paged = new PagedResult<ReportedIssueNotification>(result, result.Count());
                return MapToDto(paged);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
