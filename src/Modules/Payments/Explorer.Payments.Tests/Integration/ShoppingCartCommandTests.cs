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
        public void Adds_item()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var item = new ItemDto()
            {
                ItemId = -3,
                Name = "Zimovanje na Tari",
                Price = 200,
                Type = "Tour"
            };

            // Act
            var result = ((ObjectResult)controller.AddItem(item).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.UserId.ShouldBe(-21);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.AsEnumerable().Where(c => c.Id ==-1 && c.GetTotal() == 250);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Removes_item()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var item = new ItemDto()
            {
                ItemId = -2,
                Name = "Obilazak beoradskih muzeja",
                Price = 50,
                Type = "Tour"
            };

            // Act
            var result = ((ObjectResult)controller.RemoveItem(item).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.UserId.ShouldBe(-21);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.AsEnumerable().Where(c => c.Id == -1 && c.GetTotal() == 0);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Shopping_cart_check_out()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var touristId = -21;

            // Act
            var result = (ObjectResult)controller.Checkout(touristId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        private static ShoppingCartController CreateController(IServiceScope scope)
        {
            return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildContext("-21")
            };
        }
    }
}