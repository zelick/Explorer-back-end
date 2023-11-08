using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
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
    public class ObjectRequestCommandTests : BaseStakeholdersIntegrationTest
    {
        public ObjectRequestCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates_request()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newRequest = new ObjectRequestDto
            {
                MapObjectId = 3,
                AuthorId = 1,
                Status = "OnHold"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newRequest).Result)?.Value as ObjectRequestDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.AuthorId.ShouldBe(1);
            result.MapObjectId.ShouldBe(3);
            result.Status.ShouldBe("OnHold");
        }

        /*[Fact]
        public void Accept_Request()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var request = new ObjectRequest(1,1,RequestStatus.OnHold)

            // Act
            var result = (OkResult)controller.AcceptRequest(request.Id);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedMapObject = dbContext.MapObjects.FirstOrDefault(i => i.Id == -3);
            storedMapObject.ShouldBeNull();
        }*/

        private static ObjectRequestController CreateController(IServiceScope scope)
        {
            return new ObjectRequestController(scope.ServiceProvider.GetRequiredService<IObjectRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
