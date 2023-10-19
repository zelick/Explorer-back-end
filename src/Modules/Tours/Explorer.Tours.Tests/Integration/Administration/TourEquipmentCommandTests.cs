using Explorer.API.Controllers.Author.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourEquipmentCommandTests : BaseToursIntegrationTest
    {
        public TourEquipmentCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Add_tour_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -2;
            var equipmentId = -1;

            // Act
            var result = (OkResult)controller.AddEquipment(tourId, equipmentId);

            // Assert - Response
            result.StatusCode.ShouldBe(200);

        }

        [Fact]
        public void Remove_tour_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -1;
            var equipmentId = -1;

            // Act
            var result = (OkResult)controller.RemoveEquipment(tourId, equipmentId);

            // Assert - Response
            result.StatusCode.ShouldBe(200);

        }

        [Fact]
        public void Add_not_exists_tour()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -10;
            var equipmentId = -2;

            // Act
            var result = controller.AddEquipment(tourId, equipmentId);

            // Assert - Response
            var statusCode = (result as ObjectResult)?.StatusCode;
            statusCode.ShouldBe(404);

        }

        [Fact]
        public void Remove_not_exists_tour()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -10;
            var equipmentId = -1;

            // Act
            var result = controller.RemoveEquipment(tourId, equipmentId);

            // Assert - Response
            var statusCode = (result as ObjectResult)?.StatusCode;
            statusCode.ShouldBe(404);

        }

        [Fact]
        public void Add_not_exists_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -1;
            var equipmentId = -21;

            // Act
            var result = controller.AddEquipment(tourId, equipmentId);

            // Assert - Response
            var statusCode = (result as ObjectResult)?.StatusCode;
            statusCode.ShouldBe(404);

        }

        [Fact]
        public void Remove_not_exists_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -1;
            var equipmentId = -10;

            // Act
            var result = controller.RemoveEquipment(tourId, equipmentId);

            // Assert - Response
            var statusCode = (result as ObjectResult)?.StatusCode;
            statusCode.ShouldBe(404);

        }

        [Fact]
        public void Tour_already_have_same_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourId = -1;
            var equipmentId = -1;

            // Act
            var result = controller.AddEquipment(tourId, equipmentId);

            // Assert - Response
            var statusCode = (result as ObjectResult)?.StatusCode;
            statusCode.ShouldBe(404);

        }


        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
