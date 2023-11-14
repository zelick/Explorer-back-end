using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class ReportedIssueCommandTests : BaseToursIntegrationTest
    {
        public ReportedIssueCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new ReportedIssueDto
            {
                Category = "problem",
                Description = "Description",
                Priority = 1,
                TourId = -1,
                TouristId = -1,
                Time = DateTime.Now.ToUniversalTime(),
                Deadline = null,
                Comments = new List<ReportedIssueCommentDto>(),
                Resolved = false,
                Closed = false,
                Tour = null,
                PersonName = "",
                ProfilePictureUrl = ""
    };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity.Category, newEntity.Description, newEntity.Priority, newEntity.TourId, newEntity.TouristId).Result)?.Value as ReportedIssueDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Category.ShouldBe(newEntity.Category);

            // Assert - Database
            var storedEntity = dbContext.ReportedIssues.FirstOrDefault(i => i.Category == newEntity.Category);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Resolve()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.Resolve(-1).Result)?.Value as ReportedIssueDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Resolved.ShouldBeTrue();

            // Assert - Database
            var storedEntity = dbContext.ReportedIssues.FirstOrDefault(i => i.Resolved);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void AddComment()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            ReportedIssueCommentDto comment = new ReportedIssueCommentDto{
                Text = "Zaboravio sam",
                CreationTime= DateTime.UtcNow,
                CreatorId = 1,
                PersonName = "",
                ProfilePictureUrl = ""
            };

            // Act
            var result = ((ObjectResult)controller.AddComment(comment, -1).Result)?.Value as ReportedIssueDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Comments.Count.ShouldBe(1);

            // Assert - Database
            var storedEntity = dbContext.ReportedIssues.FirstOrDefault(i => i.Id==-1);
            storedEntity.ShouldNotBeNull();
            storedEntity.Comments.Count.ShouldBe(1);

            // Assert - generate notification
            var generatedNotif = dbContext.ReportedIssueNotifications.FirstOrDefault(notif => notif.ReportedIssueId == result.Id && notif.Description.StartsWith("You have new comment"));
            generatedNotif.ShouldNotBeNull();
        }

        [Fact]
        public void Create_fails_missing_category()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ReportedIssueDto
            {
                Description = "Description",
                Priority = 1,
                TourId = -1,
                TouristId = -1,
                Time = DateTime.Now.ToUniversalTime()
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity.Category, updatedEntity.Description, updatedEntity.Priority, updatedEntity.TourId, updatedEntity.TouristId).Result;

            // Assert
            result.StatusCode.ShouldBe(400);
        }




        private static ReportingIssueController CreateController(IServiceScope scope)
        {
            return new ReportingIssueController(scope.ServiceProvider.GetRequiredService<IReportingIssueService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
*/