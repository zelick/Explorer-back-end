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
	public class TourBundleCommandTests : BaseToursIntegrationTest
	{
		public TourBundleCommandTests(ToursTestFactory factory) : base(factory) { }

		[Fact]
		public void Creates()
		{
			// Arrange
			var controller = CreateController();
			var newEntity = new TourBundleDto();

			// Act
			var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourBundleDto;

			// Assert - Response
			result.ShouldNotBeNull();
		}

		private static TourBundleController CreateController()
		{
			return new TourBundleController()
			{
				ControllerContext = BuildContext("-12")
			};
		}
	}
}
