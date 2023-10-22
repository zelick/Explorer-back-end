using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    public class ClubRequestsTest: BaseStakeholdersIntegrationTest
    {
        public ClubRequestsTest(StakeholdersTestFactory factory) : base(factory) { }

        [Fact] //get all test
        public void GetAll_Test()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ClubRequestDto>;

            // Assert
            result.ShouldNotBeNull();
            //Ovo proveriti!
          //  result.Results.Count.ShouldBe(4);
           // result.TotalCount.ShouldBe(4); //zakomentarisano zbog brisanja  
        }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ClubRequestDto
            {
                id = 5,
                ClubId = 5,
                TouristId = 5,
                Status = "status"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubRequestDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.id.ShouldNotBe(0);
            result.ClubId.ShouldBe(newEntity.ClubId);

            // Assert - Database
            var storedEntity = dbContext.Requests.FirstOrDefault(i => i.Status == newEntity.Status);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubRequestDto
            {
                //id = -1000,
                //Status = "Neuspeli test"
                ClubId = -1000
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
            var updatedEntity = new ClubRequestDto
            {
                id = -1,
                ClubId = 5,
                TouristId = 5,
                Status = "IZMENA STATUSA UPDATE"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ClubRequestDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.id.ShouldBe(-1);
            result.Status.ShouldBe(updatedEntity.Status);

            // Assert - Database
            var storedEntity = dbContext.Requests.FirstOrDefault(i => i.Status == "IZMENA STATUSA UPDATE");
         
            var oldEntity = dbContext.Requests.FirstOrDefault(i => i.Status == "status1");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubRequestDto
            {
                id = -1000,
                ClubId = 5,
                TouristId = 5,
                Status = "IZMENA STATUSA UPDATE"
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
            var storedCourse = dbContext.Clubs.FirstOrDefault(i => i.Id == -3);
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

        private static ClubRequestController CreateController(IServiceScope scope)
        {
            return new ClubRequestController(scope.ServiceProvider.GetRequiredService<IClubRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
