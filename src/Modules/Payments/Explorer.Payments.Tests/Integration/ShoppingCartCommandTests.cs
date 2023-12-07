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
            var controller = CreateController(scope, "-23");
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
            result.Id.ShouldBe(-3);
            result.UserId.ShouldBe(-23);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.AsEnumerable().Where(c => c.Id == -3 && c.GetTotal() == 250);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Removes_item()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, "-23");
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
            result.Id.ShouldBe(-3);
            result.UserId.ShouldBe(-23);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.AsEnumerable().Where(c => c.Id == -3 && c.GetTotal() == 0);
            storedEntity.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(-21, "ABC123BC", 455)]
        [InlineData(-22, "", 100)]

        public void Shopping_cart_check_out_succeeds(int touristId, string couponCode, int exceptedWalletBalance)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, touristId.ToString());
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Act
            var result = (ObjectResult)controller.Checkout(touristId, couponCode).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var wallet = dbContext.TouristWallets.AsEnumerable().First(w => w.UserId == touristId);
            wallet.ShouldNotBeNull();
            wallet.AdventureCoins.ShouldBe(exceptedWalletBalance);
        }

        [Fact]
        public void Shopping_cart_check_out_fails_insufficient_funds()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, "-23");
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        
            var touristId = -23;
        
            // Act
            var result = (ObjectResult)controller.Checkout(touristId, "").Result;
        
            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(402);
        }


        private static ShoppingCartController CreateController(IServiceScope scope, string personId = "-21")
        {
            return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildContext(personId)
            };
        }
    }
}