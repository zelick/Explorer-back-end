using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
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
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var tourId = -1;
            var equipmentId = -1;

            // Act
            var result = (ObjectResult)controller.AddEquipment(tourId, equipmentId);

            // Assert - Response
            result.StatusCode.ShouldBe(200);

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
