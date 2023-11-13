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
    public class PublicMapObjectCommandTests : BaseToursIntegrationTest
    {
        public PublicMapObjectCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreatePublicMapObjectController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.Create(-1,"kom").Result)?.Value as PublicMapObjectDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe("Location 1");

            // Assert - Database
            var storedMapObject = dbContext.PublicMapObjects.FirstOrDefault(i => i.Name == "Location 1");
            storedMapObject.ShouldNotBeNull();
            storedMapObject.Id.ShouldBe(result.Id);
        }

        private static PublicMapObjectController CreatePublicMapObjectController(IServiceScope scope)
        {
            return new PublicMapObjectController(scope.ServiceProvider.GetRequiredService<IPublicObjectService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
