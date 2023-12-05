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
	public class TourBundleQueryTests : BaseToursIntegrationTest
	{
		public TourBundleQueryTests(ToursTestFactory factory) : base(factory) { }

		[Fact]
		public void Retrieves_all()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);

			// Act
			var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourBundleDto>;

			// Assert
			result.ShouldNotBeNull();
			result.Results.Count.ShouldBe(3);
		}

        [Fact]
        public void Retrieves_all_published()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAllPublished(0, 0).Result)?.Value as PagedResult<TourBundleDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(1);
        }

        private static TourBundleController CreateController(IServiceScope scope)
		{
			return new TourBundleController(scope.ServiceProvider.GetRequiredService<ITourBundleService>())
			{
				ControllerContext = BuildContext("-12")
			};
		}
	}
}
