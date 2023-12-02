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
    public class PrivateTourCommandTests: BaseToursIntegrationTest
    {
        public PrivateTourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void createPrivateTour_Succeed()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            PrivateTourDto privateTourDto= new PrivateTourDto();
            privateTourDto.Name = "test";
            privateTourDto.TouristId = -5;
            privateTourDto.Checkpoints = new List<PublicCheckpointDto>();
            // Act
            var result = ((ObjectResult)controller.CreatePrivateTour(privateTourDto).Result)?.Value as PrivateTourDto;
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
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
