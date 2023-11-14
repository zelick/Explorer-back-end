using Explorer.API.Controllers.Tourist.Tour;
using Explorer.API.Controllers.Tourist.Tourism;
using Explorer.Stakeholders.Infrastructure.Database;
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

namespace Explorer.Tours.Tests.Integration.Tourism
{
    [Collection("Sequential")]
    public class PublicTourCommandTests : BaseToursIntegrationTest
    {

        public PublicTourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void getAllPublicTours_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.GetPublicTours().Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        private static PublicTourController CreateController(IServiceScope scope)
        {
            return new PublicTourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
