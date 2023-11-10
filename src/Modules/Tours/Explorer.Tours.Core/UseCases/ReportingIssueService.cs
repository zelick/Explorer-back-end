using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ReportingIssueService : CrudService<ReportedIssueDto, ReportedIssue>, IReportingIssueService
    {
        private readonly IReportedIssueRepository _reportedIssuesRepository;
        private readonly IReportedIssueNotificationRepository _reportedIssueNotificationRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IInternalPersonService _personService;
        public ReportingIssueService(ICrudRepository<ReportedIssue> repository, IMapper mapper,
                                     IReportedIssueRepository issuerepo, 
                                     IReportedIssueNotificationRepository reportedIssueNotificationRepository,
                                     ITourRepository tourRepository, IInternalPersonService personService) : base(repository, mapper)
        {
            _reportedIssuesRepository = issuerepo;
            _reportedIssueNotificationRepository = reportedIssueNotificationRepository;
            _tourRepository = tourRepository;
            _personService = personService;
        }
        public Result<ReportedIssueDto> Resolve(long id)
        {
            try
            {
                var result = _reportedIssuesRepository.Resolve(id);
                var dto = MapToDto(result);
                LoadPerson(dto);
                return dto;
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
                var dto = MapToDto(result);
                LoadPerson(dto);
                GenerateNotification(dto, reportedISsueComment.CreatorId);
                return dto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        private void GenerateNotification(ReportedIssueDto reportedIssue, int commentCreatorId)
        {
            // generate notification for new comment
            if (commentCreatorId == reportedIssue.TouristId)
            {
                var description = "" + "You have new comment for your issue: " + reportedIssue.Description + " in tour " + reportedIssue.Tour.Name + "!";
                var notifTourist = _reportedIssueNotificationRepository.Create(description, reportedIssue.Tour.AuthorId, reportedIssue.Id);
            }
            else
            {
                var description = "You have new comment for your issue: " + reportedIssue.Description + " in tour " + reportedIssue.Tour.Name + "!";
                var notifAuthor = _reportedIssueNotificationRepository.Create(description, reportedIssue.TouristId, reportedIssue.Id);
            }
        }

        public Result<ReportedIssueDto> AddDeadline(int id, DateTime deadline)
        {
            try
            {
                var issue = _reportedIssuesRepository.Get(id);
                var result = _reportedIssuesRepository.AddDeadline(id, deadline);
                var dto = MapToDto(result);
                LoadPerson(dto);
                var description = "New deadline added on your issue: " + dto.Description + ". Please deal with this problem before " + deadline.ToString("dd.MM.yyyy") + "!";
                _reportedIssueNotificationRepository.Create(description, issue.Tour.AuthorId, id);
                return dto;
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
                var dto = MapToDto(issue);
                LoadPerson(dto);
                return dto;
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
                var dto = MapToDto(result);
                LoadPerson(dto);
                return dto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<ReportedIssueDto>> GetPaged(int page, int pageSize)
        {
            try
            {
                var result = _reportedIssuesRepository.GetPaged(page, pageSize);
                var list =  MapToDto(result);
                AddPersonsInfo(list.Value.Results);
                return list;
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
                var list = MapToDto(result);
                AddPersonsInfo(list.Value.Results);
                return list;
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
                var list = MapToDto(result);
                AddPersonsInfo(list.Value.Results);
                return list;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        private void AddPersonsInfo(List<ReportedIssueDto> reportedIssues)
        {
            foreach (var reportedIssue in reportedIssues)
            {
                LoadPerson(reportedIssue);
            }
        }

        private void LoadPerson(ReportedIssueDto reportedIssue)
        {
            var person = _personService.GetByUserId(reportedIssue.TouristId);

            if (person != null)
            {
                reportedIssue.PersonName = person.Name + " " + person.Surname;
                reportedIssue.ProfilePictureUrl = person.ProfilePictureUrl;
            }

            foreach (var comment in reportedIssue.Comments)
            {
                var person1 = _personService.GetByUserId(comment.CreatorId);

                if (person1 != null)
                {
                    comment.PersonName = person1.Name + " " + person1.Surname;
                    comment.ProfilePictureUrl = person1.ProfilePictureUrl;
                }
            }
        }

    }
}
