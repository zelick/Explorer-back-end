using Explorer.API.Controllers.Tourist.Tourism;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Shopping;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Http;
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
    [Collection("Sequential")]
    public class ShoppingCartCommandTests : BaseStakeholdersIntegrationTest
    {
        public ShoppingCartCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ShoppingCartDto
            {
                TouristId = -23,
                Items = null,
                Price = 0.0
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TouristId.ShouldBe(newEntity.TouristId);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.FirstOrDefault(i => i.TouristId == newEntity.TouristId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var updatedEntity = new ShoppingCartDto
            {
                Id = -1,
                TouristId = -23,
                Items = null,
                Price = 250.0
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.TouristId.ShouldBe(updatedEntity.TouristId);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.FirstOrDefault(i => i.Price == 250.0);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ShoppingCartDto
            {
                Id = -1000,
                TouristId = -21,
                Items = null,
                Price = 250.0
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
            var result = (OkResult)controller.Delete(-1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.ShoppingCarts.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Check_if_exists()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = controller.CheckShoppingCart(-21) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

        }

        [Fact]
        public void Get_shopping_cart()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = ((ObjectResult)controller.GetShoppingCart(-21).Result)?.Value as ShoppingCartDto;

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TouristId.ShouldBe(-21);

        }

        [Fact]
        public void Delete_order_item()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = ((ObjectResult)controller.DeleteOrderItems(-1).Result)?.Value as ShoppingCartDto;

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Items.Count.ShouldBe(0);

        }

        private static ShoppingCartController CreateController(IServiceScope scope)
        {
            return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}