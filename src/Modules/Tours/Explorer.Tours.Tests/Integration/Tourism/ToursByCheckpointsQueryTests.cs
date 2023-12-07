using Explorer.API.Controllers.Tourist.Tour;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
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
    public class ToursByCheckpointsQueryTests: BaseToursIntegrationTest
    {
        public ToursByCheckpointsQueryTests(ToursTestFactory factory) : base(factory) { }
        [Fact]
        public void getAllToursByPublicCheckpoints_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            List<PublicCheckpointDto> checkpoints= new List<PublicCheckpointDto>();
            PublicCheckpointDto ch1 = new PublicCheckpointDto();
            ch1.Pictures = new List<string>();
            ch1.Name= "1";
            ch1.Longitude = 20.4252;
            ch1.Latitude = 44.7953;
            ch1.Description= "1";
            checkpoints.Add(ch1);
            // Act
            var result = ((ObjectResult)controller.GetToursByPublicCheckpoints(checkpoints).Result)?.Value as List<PublicTourDto>;
            result.ShouldNotBeNull();
            //result.Count.ShouldBe(2);
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
