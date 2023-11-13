using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
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
    public class ObjectRequestQueryTests : BaseStakeholdersIntegrationTest
    {
        public ObjectRequestQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll().Result)?.Value as List<ObjectRequestDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }
        private static ObjectRequestController CreateController(IServiceScope scope)
        {
            return new ObjectRequestController(scope.ServiceProvider.GetRequiredService<IObjectRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
