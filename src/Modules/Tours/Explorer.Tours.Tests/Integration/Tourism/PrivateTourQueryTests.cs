using Explorer.API.Controllers.Tourist.Tour;
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

namespace Explorer.Tours.Tests.Integration.Tourism
{
    [Collection("Sequential")]
    public class PrivateTourQueryTests : BaseToursIntegrationTest
    {
        public PrivateTourQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void getAllByTourist_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.GetPrivateTours(-3).Result)?.Value as List<PrivateTourDto>;
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        private static PrivateTourController CreateController(IServiceScope scope)
        {
            return new PrivateTourController(scope.ServiceProvider.GetRequiredService<IPrivateTourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
