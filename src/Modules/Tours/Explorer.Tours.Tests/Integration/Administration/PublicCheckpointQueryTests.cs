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
    public class PublicCheckpointQueryTests : BaseToursIntegrationTest
    {
        public PublicCheckpointQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void RetrievesAll()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreatePublicCheckpointController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll().Result)?.Value as PagedResult<PublicCheckpointDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        [Fact]
        public void FindAllAtPlace()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreatePublicCheckpointController(scope);

            var result = ((ObjectResult)controller.GetAllAtPlace(45.0, 45.0).Result)?.Value as PagedResult<PublicCheckpointDto>;

            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }

        private static PublicCheckpointController CreatePublicCheckpointController(IServiceScope scope)
        {
            return new PublicCheckpointController(scope.ServiceProvider.GetRequiredService<IPublicCheckpointService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
