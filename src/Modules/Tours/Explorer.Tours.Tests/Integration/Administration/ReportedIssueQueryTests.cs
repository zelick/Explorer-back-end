using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Set_deadline()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            DateTime currentDate = DateTime.UtcNow.AddDays(6);


            // Act
            var result = ((ObjectResult)controller.AddDeadline(-1, currentDate).Result).Value as ReportedIssueDto;

            // Assert
            result.ShouldNotBeNull();
            result.Deadline.ShouldBe(currentDate);
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
    }
}
