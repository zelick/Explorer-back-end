using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    public class ReportedIssueQueryTests : BaseToursIntegrationTest
    {
        public ReportedIssueQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ReportedIssueDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        [Fact]
        public void Retrieves_all_by_tourist()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateTouristController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAllByTourist(2, 0, 0).Result)?.Value as PagedResult<ReportedIssueDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }

        [Fact]
        public void Retrieves_all_by_author()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateAuthorController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAllByAuthor(2, 0, 0).Result)?.Value as PagedResult<ReportedIssueDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }

        [Fact]
        public void Set_deadline()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            DateTime currentDate = DateTime.UtcNow.AddDays(6);


            // Act
            var result = ((ObjectResult)controller.AddDeadline(-1, currentDate).Result).Value as ReportedIssueDto;

            // Assert
            result.ShouldNotBeNull();
            result.Deadline.ShouldBe(currentDate);

            // Assert - generate notification
            var generatedNotif = dbContext.ReportedIssueNotifications.FirstOrDefault(notif => notif.ReportedIssueId == result.Id && notif.Description.StartsWith("New deadline added"));
            generatedNotif.ShouldNotBeNull();
        }

        [Fact]
        public void Close_reported_issue()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);


            // Act
            var result = ((ObjectResult)controller.Close(-1).Result).Value as ReportedIssueDto;

            // Assert
            result.ShouldNotBeNull();
            result.Closed.ShouldBe(true);
        }

        [Fact]
        public void Penalize_author_of_reported_issue()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);


            // Act
            var result = ((ObjectResult)controller.PenalizeAuthor(-1).Result).Value as ReportedIssueDto;

            // Assert
            result.ShouldNotBeNull();
            result.Tour.Closed.ShouldBe(true);
        }

        private static ReportedIssuesReviewController CreateController(IServiceScope scope)
        {
            return new ReportedIssuesReviewController(scope.ServiceProvider.GetRequiredService<IReportingIssueService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        private static ReportingIssueController CreateTouristController(IServiceScope scope)
        {
            return new ReportingIssueController(scope.ServiceProvider.GetRequiredService<IReportingIssueService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        private static ReportedIssueRespondingController CreateAuthorController(IServiceScope scope)
        {
            return new ReportedIssueRespondingController(scope.ServiceProvider.GetRequiredService<IReportingIssueService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
