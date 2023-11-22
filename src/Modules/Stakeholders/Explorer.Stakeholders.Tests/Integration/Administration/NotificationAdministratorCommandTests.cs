using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    public class NotificationAdministratorCommandTests : BaseStakeholdersIntegrationTest
    {
        public NotificationAdministratorCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new NotificationDto
            {
                Id = -1,
                Description = "Admins old notification!!!",
                IsRead = true,
                CreationTime = DateTime.UtcNow,
                UserId = -1,
                ForeignId = -1
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as NotificationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Description.ShouldBe(updatedEntity.Description);
            result.IsRead.ShouldBe(updatedEntity.IsRead);

            // Assert - Database
            var storedEntity = dbContext.Notifications.FirstOrDefault(i => i.Description == "Admins old notification!!!");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Notifications.FirstOrDefault(i => i.Description == "Notification 1!");
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
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Notifications.FirstOrDefault(i => i.Id == -2);
            storedCourse.ShouldBeNull();
        }

        private static NotificationAdministratorController CreateController(IServiceScope scope)
        {
            return new NotificationAdministratorController(scope.ServiceProvider.GetRequiredService<INotificationService>());
        }
    }
}
