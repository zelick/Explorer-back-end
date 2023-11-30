using Explorer.API.Controllers.Author.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Author
{
    public class NotificationAuthorCommandTests : BaseStakeholdersIntegrationTest
    {
        public NotificationAuthorCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new NotificationDto
            {
                Id = -3,
                Description = "Author old notification!!!",
                IsRead = true,
                CreationTime = DateTime.UtcNow,
                UserId = -12,
                ForeignId = -1
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as NotificationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-3);
            result.Description.ShouldBe(updatedEntity.Description);
            result.IsRead.ShouldBe(updatedEntity.IsRead);

            // Assert - Database
            var storedEntity = dbContext.Notifications.FirstOrDefault(i => i.Description == "Author old notification!!!");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Notifications.FirstOrDefault(i => i.Description == "Notification 2!");
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
            var result = (OkResult)controller.Delete(-4);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Notifications.FirstOrDefault(i => i.Id == -4);
            storedCourse.ShouldBeNull();
        }

        private static NotificationAuthorController CreateController(IServiceScope scope)
        {
            return new NotificationAuthorController(scope.ServiceProvider.GetRequiredService<INotificationService>());
        }
    }
}
