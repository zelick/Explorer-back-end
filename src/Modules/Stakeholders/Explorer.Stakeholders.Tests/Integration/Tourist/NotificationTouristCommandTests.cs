using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    public class NotificationTouristCommandTests : BaseStakeholdersIntegrationTest
    {
        public NotificationTouristCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new NotificationDto
            {
                Id = -6,
                Description = "Tourist old notification!!!",
                IsRead = true,
                CreationTime = DateTime.UtcNow,
                UserId = -21,
                ForeignId = -2
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as NotificationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-6);
            result.Description.ShouldBe(updatedEntity.Description);
            result.IsRead.ShouldBe(updatedEntity.IsRead);

            // Assert - Database
            var storedEntity = dbContext.Notifications.FirstOrDefault(i => i.Description == "Tourist old notification!!!");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Notifications.FirstOrDefault(i => i.Description == "Notification 5!");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-5);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Notifications.FirstOrDefault(i => i.Id == -5);
            storedCourse.ShouldBeNull();
        }

        private static NotificationTouristController CreateController(IServiceScope scope)
        {
            return new NotificationTouristController(scope.ServiceProvider.GetRequiredService<INotificationService>());
        }
    }
}
