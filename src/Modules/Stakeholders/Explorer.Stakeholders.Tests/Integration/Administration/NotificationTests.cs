using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist.Tour;
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
    public class NotificationTests : BaseStakeholdersIntegrationTest
    {
        public NotificationTests(StakeholdersTestFactory factory) : base(factory)
        {

        }

        [Fact]
        public void Get_all_unread()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            //Act
            var result = ((ObjectResult)controller.GetAllUnread(-1).Result)?.Value as List<NotificationDto>;

            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        [Fact]
        public void Mark_as_read()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            //Act
            var result = ((ObjectResult)controller.MarkAsRead(-3).Result)?.Value as NotificationDto;

            //Assert
            result.ShouldNotBeNull();
            result.IsRead.ShouldBeTrue();
        }

        private static NotificationController CreateController(IServiceScope scope)
        {
            return new NotificationController(scope.ServiceProvider.GetRequiredService<INotificationService>());
        }

    }
}
