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
			result.Id.ShouldNotBe(0);
			result.Name.ShouldBe(newEntity.Name);
			result.Price.ShouldBe(newEntity.Price);
			result.AuthorId.ShouldBe(newEntity.AuthorId);
			result.Status.ShouldBe(newEntity.Status);

			var storedEntity = dbContext.TourBundles.OrderBy(i => i.Id).LastOrDefault(i => i.Name == newEntity.Name);
			storedEntity.ShouldNotBeNull();
			storedEntity.Id.ShouldBe(result.Id);
		}

		
		[Fact]
		public void Updates()
		{
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
			var updatedEntity = new TourBundleDto
			{
				Id = -2,
				Name = "Paketi tura 3",
				Price = 252.0, 
				AuthorId = 2,
				Status = "Draft"
			};

			var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourBundleDto;

			result.ShouldNotBeNull();
			result.Id.ShouldBe(-2);
			result.Name.ShouldBe(updatedEntity.Name);
			result.Price.ShouldBe(updatedEntity.Price);
			result.AuthorId.ShouldBe(updatedEntity.AuthorId);
			result.Status.ShouldBe(updatedEntity.Status);

			var storedEntity = dbContext.TourBundles.FirstOrDefault(i => i.Name == updatedEntity.Name);
			storedEntity.ShouldNotBeNull();
			storedEntity.Id.ShouldBe(result.Id);
		}

		[Fact]
		public void Deletes()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

			// Act
			var result = (OkResult)controller.Delete(-1);

			// Assert - Response
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(200);
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
