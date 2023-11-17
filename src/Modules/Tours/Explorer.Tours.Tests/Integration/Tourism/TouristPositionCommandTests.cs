using Explorer.API.Controllers.Tourist.Tourism;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourism
{
    [Collection("Sequential")]

    public class TouristPositionCommandTests : BaseToursIntegrationTest
    {
        public TouristPositionCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TouristPositionDto
            {
                Id = 1,
                CreatorId = 1,
                Latitude = 45,
                Longitude = 45
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TouristPositionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.CreatorId.ShouldBe(newEntity.CreatorId);
            result.CreatorId.ShouldNotBe(0);

            // Assert - Database
            var storedEntity = dbContext.TouristPosition.FirstOrDefault(i => i.Id == newEntity.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TouristPositionDto
            {
                Latitude = 91,
            };


            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TouristPositionDto
            {
                Id = -1,
                CreatorId = 3,
                Latitude = 4,
                Longitude = 4,
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TouristPositionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.CreatorId.ShouldBe(updatedEntity.CreatorId);
            result.Latitude.ShouldBe(updatedEntity.Latitude);
            result.Longitude.ShouldBe(updatedEntity.Longitude);

            // Assert - Database
            var storedEntity = dbContext.TouristPosition.FirstOrDefault(i => i.CreatorId == 3);
            storedEntity.ShouldNotBeNull();
            storedEntity.CreatorId.ShouldBe(updatedEntity.CreatorId);
            var oldEntity = dbContext.TouristPosition.FirstOrDefault(i => i.CreatorId == 2);
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TouristPositionDto
            {
                Id = -10,
                CreatorId = 2,
                Latitude = 1,
                Longitude = 1,
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.TouristPosition.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static TouristPositionController CreateController(IServiceScope scope)
        {
            return new TouristPositionController(scope.ServiceProvider.GetRequiredService<ITouristPositionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}