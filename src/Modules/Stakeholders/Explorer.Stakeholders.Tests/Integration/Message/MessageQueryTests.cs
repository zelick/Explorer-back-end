using Explorer.API.Controllers.User.SocialProfile;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Message
{
    [Collection("Sequential")]
    public class MessageQueryTests : BaseStakeholdersIntegrationTest
    {

        public MessageQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_sent()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int id = -13;

            // Act
            var result = ((ObjectResult)controller.GetAllSent(id).Result)?.Value as List<MessageDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public void Retrieves_inbox()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int id = -13;

            // Act
            var result = ((ObjectResult)controller.GetInbox(id).Result)?.Value as List<MessageDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }

        [Fact]
        public void Retrieves_notifications()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int id = -13;

            // Act
            var result = ((ObjectResult)controller.GetNotifications(id).Result)?.Value as List<MessageDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }


        private static MessageController CreateController(IServiceScope scope)
        {
            return new MessageController(scope.ServiceProvider.GetRequiredService<IMessageService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
