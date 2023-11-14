/*using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Author
{
    [Collection("Sequential")]
    public class CheckpointRequestCommandTests : BaseStakeholdersIntegrationTest
    {
        public CheckpointRequestCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates_request()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newRequest = new CheckpointRequestDto
            {
                CheckpointId = 3,
                AuthorId = 1,
                Status = "OnHold"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newRequest).Result)?.Value as CheckpointRequestDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.AuthorId.ShouldBe(1);
            result.CheckpointId.ShouldBe(3);
            result.Status.ShouldBe("OnHold");
        }

        [Fact]
        public void Accept_Request()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = ((ObjectResult)controller.AcceptRequest(-1).Result)?.Value as CheckpointRequestDto; ;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Status.ShouldBe("Accepted");

            // Assert - Database
            var storedRequest = dbContext.CheckpointRequests.FirstOrDefault(i => i.Id == -1);
            storedRequest.Status.ShouldBe(RequestStatus.Accepted);
        }

        [Fact]
        public void Reject_Request()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = ((ObjectResult)controller.RejectRequest(-1).Result)?.Value as CheckpointRequestDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Status.ShouldBe("Rejected");

            // Assert - Database
            var storedRequest = dbContext.CheckpointRequests.FirstOrDefault(i => i.Id == -1);
            storedRequest.Status.ShouldBe(RequestStatus.Rejected);
        }

        private static CheckpointRequestController CreateController(IServiceScope scope)
        {
            return new CheckpointRequestController(scope.ServiceProvider.GetRequiredService<ICheckpointRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
*/