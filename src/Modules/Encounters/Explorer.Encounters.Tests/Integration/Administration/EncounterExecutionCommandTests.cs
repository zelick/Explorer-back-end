using Explorer.API.Controllers.Tourist.Encounters;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Encounters.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class EncounterExecutionCommandTests : BaseEncountersIntegrationTest
    {
        public EncounterExecutionCommandTests(EncountersTestFactory factory) : base(factory) { }
        [Fact]
        public void ActivateEncounter_success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
            var expectedResponseCode = 200;
            var id = -1;
            var longitude = 20;
            var latitude = 45;

            // Act
            var result = (ObjectResult)controller.Activate(id, longitude, latitude).Result;

            //Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        [Fact]
        public void CheckPosition_success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
            var expectedResponseCode = 200;
            var id = -1;
            var longitude = 20;
            var latitude = 45;

            // Act
            var result = (ObjectResult)controller.CheckPosition(id, longitude, latitude).Result;

            //Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        [Fact]
        public void CheckPosition_not_in_range()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
            var expectedResponseCode = 400;
            var id = -1;
            var longitude = 10;
            var latitude = 40;

            // Act
            var result = (ObjectResult)controller.CheckPosition(id, longitude, latitude).Result;

            //Assert
            result.ShouldNotBeNull();
            result.Value.ShouldBe(0);
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        private static EncounterExecutionController CreateController(IServiceScope scope)
        {
            return new EncounterExecutionController(scope.ServiceProvider.GetRequiredService<IEncounterExecutionService>(), scope.ServiceProvider.GetRequiredService<IEncounterService>())
            {
                ControllerContext = BuildContext("-21")
            };
        }
    }
}
