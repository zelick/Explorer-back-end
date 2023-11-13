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

namespace Explorer.Stakeholders.Tests.Integration.Author
{
    [Collection("Sequential")]
    public class CheckpointRequestQueryTests : BaseStakeholdersIntegrationTest
    {
        public CheckpointRequestQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll().Result)?.Value as List<CheckpointRequestDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
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
