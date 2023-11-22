using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    public class NotificationAdministratorQueryTests : BaseStakeholdersIntegrationTest
    {
        public NotificationAdministratorQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_AllByUser()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAllByUser(-12, 0, 0).Result)?.Value as PagedResult<NotificationDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        [Fact]
        public void Retrieves_UnreadByUser()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetUnreadByUser(-12, 0, 0).Result)?.Value as PagedResult<NotificationDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(1);
            result.TotalCount.ShouldBe(1);
        }

        private static NotificationAdministratorController CreateController(IServiceScope scope)
        {
            return new NotificationAdministratorController(scope.ServiceProvider.GetRequiredService<INotificationService>());
        }
    }
}
