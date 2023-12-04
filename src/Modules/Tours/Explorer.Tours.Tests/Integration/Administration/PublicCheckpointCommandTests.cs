using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class PublicChekpointCommandTests : BaseToursIntegrationTest
    {
        public PublicChekpointCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreatePublicMapObjectController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.Create(-1, "kom").Result)?.Value as PublicCheckpointDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe("uuuu");

            // Assert - Database
            var storedMapObject = dbContext.PublicCheckpoint.FirstOrDefault(i => i.Name == "uuuu");
            storedMapObject.ShouldNotBeNull();
            storedMapObject.Id.ShouldBe(result.Id);
        }

        private static PublicCheckpointController CreatePublicMapObjectController(IServiceScope scope)
        {
            return new PublicCheckpointController(scope.ServiceProvider.GetRequiredService<IPublicCheckpointService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
