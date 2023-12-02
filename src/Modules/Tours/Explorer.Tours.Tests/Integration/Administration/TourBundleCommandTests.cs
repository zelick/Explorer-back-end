using Explorer.API.Controllers.Author.Administration;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Infrastructure.Database;
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
	public class TourBundleCommandTests : BaseToursIntegrationTest
	{
		public TourBundleCommandTests(ToursTestFactory factory) : base(factory) { }

		[Fact]
		public void Creates()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
			var newEntity = new TourBundleDto
			{
				Name = "Paket tura 1",
				Price = 250.0,
				AuthorId = 2,
				Status = "Draft"
			};

			// Act
			var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourBundleDto;

			// Assert - Response
			result.ShouldNotBeNull();
		}

		[Fact]
		public void Create_fails_invalid_data()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var updatedEntity = new TourBundleDto
			{
				Price = 2.0,
				AuthorId = 1,
				Status = "Draft"
			};

			// Act
			var result = (ObjectResult)controller.Create(updatedEntity).Result;

			// Assert
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(500);
		}

		[Fact]
		public void Updates()
		{
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
			var updatedEntity = new TourBundleDto();

			var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourBundleDto;

			result.ShouldNotBeNull();
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
