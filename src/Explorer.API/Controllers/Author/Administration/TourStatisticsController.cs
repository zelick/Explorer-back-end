using Explorer.Blog.Core.Domain.BlogPosts;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tourStatistics")]
    public class TourStatisticsController : BaseApiController
    {
        private readonly ITourStatisticsService _tourStatisticsService;
        public TourStatisticsController(ITourStatisticsService tourStatisticsService)
        {
            _tourStatisticsService = tourStatisticsService;
        }

        [HttpGet("soldToursNumber/{authorId:int}")]
        public ActionResult<int> GetAuthorsSoldToursNumber(long authorId)
        {
            if (User.PersonId() != authorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

            var result = _tourStatisticsService.GetAuthorsSoldToursNumber(authorId);
            return CreateResponse(result);
        }

        [HttpGet("startedToursNumber/{authorId:int}")]
        public ActionResult<int> GetAuthorsStartedToursNumber(long authorId)
        {
            if (User.PersonId() != authorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

            var result = _tourStatisticsService.GetAuthorsStartedToursNumber(authorId);
            return CreateResponse(result);
        }

        [HttpGet("finishedToursNumber/{authorId:int}")]
        public ActionResult<int> GetAuthorsFinishedToursNumber(long authorId)
        {
            if (User.PersonId() != authorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

            var result = _tourStatisticsService.GetAuthorsFinishedToursNumber(authorId);
            return CreateResponse(result);
        }

        [HttpGet("tourCompletitionPercentage/{authorId:int}")]
        public ActionResult<double> GetAuthorsTourCompletionPercentage(long authorId)
        {
            if (User.PersonId() != authorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

            var result = _tourStatisticsService.GetAuthorsTourCompletionPercentage(authorId);
            return CreateResponse(result);
        }
    }
}
