using Explorer.API.Controllers.Author.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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
    public class PublicMapObjectQueryTests : BaseToursIntegrationTest
    {
        public PublicMapObjectQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void RetrievesAll()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreatePublicMapObjectController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll().Result)?.Value as PagedResult<PublicMapObjectDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
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
