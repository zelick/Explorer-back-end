using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class ShoppingCartCommandTests : BasePaymentsIntegrationTest
    {
        public ShoppingCartCommandTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var updatedEntity = new ShoppingCartDto
            {
                Id = -1,
                UserId = -23,
                Items = new List<OrderItemDto>() { new() { ItemId = 1, Price = 250.0} },
                Price = 250.0
            };

            // Act
            var result = ((ObjectResult)controller.Update(-1, updatedEntity).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.UserId.ShouldBe(updatedEntity.UserId);

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
                UserId = -21,
                Items = null,
                Price = 250.0
            };

            // Act
            var result = (ObjectResult)controller.Update(-1000, updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        [Fact]
        public void Get_shopping_cart()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Act
            var result = ((ObjectResult)controller.GetByUser(-21).Result)?.Value as ShoppingCartDto;

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(-21);

        }

        [Fact]
        public void ShoppingCartCheckOut_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var customerId = -21;

            // Act
            var result = (ObjectResult)controller.Checkout(customerId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
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