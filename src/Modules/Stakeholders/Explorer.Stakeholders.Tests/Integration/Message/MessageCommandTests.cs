using Explorer.API.Controllers.User.SocialProfile;
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

namespace Explorer.Stakeholders.Tests.Integration.Message
{
    [Collection("Sequential")]
    public class MessageCommandTests : BaseStakeholdersIntegrationTest
    {
        public MessageCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Send_success()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            
            var exampleMessage = new MessageDto
            {
                Id = -5,
                SenderId = -11,
                RecipientId = -12,
                SenderUsername = "autor1@gmail.com",
                Title = "Example Message",
                SentDateTime = DateTime.UtcNow,
                ReadDateTime = null,
                Content = "This is an example message content.",
                IsRead = false
            };

            // Act
            var result = ((ObjectResult)controller.SendMessage(exampleMessage).Result)?.Value as MessageDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-5);
        }


        [Fact]
        public void Send_failed()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var exampleMessage = new MessageDto
            {
                Id = -6,
                SenderId = -11,
                RecipientId = -23,
                SenderUsername = "autor1@gmail.com",
                Title = "Example Message",
                SentDateTime = DateTime.UtcNow,
                ReadDateTime = null,
                Content = "This is an example message content.",
                IsRead = false
            };

            // Act
            var result = (ObjectResult)controller.SendMessage(exampleMessage).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Read_success()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var id = -3;

            // Act
            var result = ((ObjectResult)controller.Read(id).Result)?.Value as MessageDto;

            // Assert
            result.ShouldNotBeNull();
            result.IsRead.ShouldBeTrue();
            result.ReadDateTime.ShouldNotBeNull();
        }

        [Fact]
        public void Update_success()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var updatedMessage = new MessageDto
            {
                Id = -1,
                SenderId = -13,
                RecipientId = -21,
                SenderUsername = "autor3@gmail.com",
                Title = "Example Message",
                SentDateTime = DateTime.UtcNow,
                ReadDateTime = null,
                Content = "This is an example message content.",
                IsRead = false
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedMessage).Result)?.Value as MessageDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(updatedMessage.Id);
        }

        [Fact]
        public void Delete_success()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var id = -1;

            // Act
            var result = (OkResult)controller.Delete(id);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
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
