using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
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
    public class TourStatisticsCommandTests: BaseToursIntegrationTest
    {
        public TourStatisticsCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void GetAuthorsSoldToursNumber()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.GetAuthorsSoldToursNumber(-11).Result)?.Value;

            // Assert - Response
            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetAuthorsStartedToursNumber()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.GetAuthorsStartedToursNumber(-11).Result)?.Value;

            // Assert - Response
            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetAuthorsFinishedToursNumber()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.GetAuthorsFinishedToursNumber(-11).Result)?.Value;

            // Assert - Response
            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetAuthorsTourCompletionPercentage()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.GetAuthorsTourCompletionPercentage(-11).Result)?.Value;

            // Assert - Response
            result.ShouldNotBeNull();
        }

        private static TourStatisticsController CreateController(IServiceScope scope)
        {
            return new TourStatisticsController(scope.ServiceProvider.GetRequiredService<ITourStatisticsService>())
            {
                ControllerContext = BuildContext("-11")
            };
        }
    }
}
