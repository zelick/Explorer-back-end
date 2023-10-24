using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Tests;
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

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
	public class ClubInvitationsTests : BaseStakeholdersIntegrationTest
	{
		public ClubInvitationsTests(StakeholdersTestFactory factory) : base(factory) { }

		[Fact]
		public void Retrieves_all()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);

			// Act
			var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ClubInvitationDto>;

			// Assert
			result.ShouldNotBeNull();
		}

		[Fact]
		public void Creates()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
			var newEntity = new ClubInvitationDto
			{
				OwnerId = -1111,
				MemberId = -1111,
				ClubId = -1111,
				Status = "Accepted"
			};

			// Act
			var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubInvitationDto;

			// Assert - Response
			result.ShouldNotBeNull();
			result.Id.ShouldNotBe(0);
			result.Status.ShouldBe(newEntity.Status);

			// Assert - Database
			var storedEntity = dbContext.ClubInvitations.FirstOrDefault(i => i.Status == newEntity.Status);
			storedEntity.ShouldNotBeNull();
			storedEntity.Id.ShouldBe(result.Id);
		}

		[Fact]
		public void Create_fails_invalid_data()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var updatedEntity = new ClubInvitationDto
			{
				Status = "Accepted"
			};

			// Act
			var result = (ObjectResult)controller.Create(updatedEntity).Result;

			// Assert
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(400);
		}

		[Fact]
		public void Updates()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
			var updatedEntity = new ClubInvitationDto
			{
				Id = -1,
				OwnerId = -21,
				MemberId = -22,
				ClubId = -2,
				Status = "Rejected"
			};

			// Act
			var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ClubInvitationDto;

			// Assert - Response
			result.ShouldNotBeNull();
			result.Id.ShouldBe(-1);
			result.Status.ShouldBe(updatedEntity.Status);

			// Assert - Database
			var storedEntity = dbContext.ClubInvitations.FirstOrDefault(i => i.Status == "Rejected");
			storedEntity.ShouldNotBeNull();
			storedEntity.Status.ShouldBe(updatedEntity.Status);
			var oldEntity = dbContext.ClubInvitations.FirstOrDefault(i => i.Status == "Accepted");
			oldEntity.ShouldBeNull();
		}

		[Fact]
		public void Update_fails_invalid_id()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var updatedEntity = new ClubInvitationDto
			{
				Id = -1000,
				OwnerId = 3,
				MemberId = 2,
				ClubId = 3,
				Status = "Rejected"
			};

			// Act
			var result = (ObjectResult)controller.Update(updatedEntity).Result;

			// Assert
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(404);
		}

		[Fact]
		public void Deletes()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

			// Act
			var result = (OkResult)controller.Delete(-3);

			// Assert - Response
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(200);

			// Assert - Database
			var storedCourse = dbContext.ClubInvitations.FirstOrDefault(i => i.Id == -3);
			storedCourse.ShouldBeNull();
		}

		[Fact]
		public void Delete_fails_invalid_id()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);

			// Act
			var result = (ObjectResult)controller.Delete(-1000);

			// Assert
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(404);
		}

		private static ClubInvitationController CreateController(IServiceScope scope)
		{
			return new ClubInvitationController(scope.ServiceProvider.GetRequiredService<IClubInvitationService>())
			{
				ControllerContext = BuildContext("-1")
			};
		}
	}
}
