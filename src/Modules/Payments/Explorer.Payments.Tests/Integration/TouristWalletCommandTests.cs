using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration
{
    public class TouristWalletCommandTests : BasePaymentsIntegrationTest
    {
        public TouristWalletCommandTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Payment_adventure_coins()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            int newAdventureCoins = 3;

            // Act
            var result = ((ObjectResult)controller.PaymentAdventureCoins(-1, newAdventureCoins).Result)?.Value as TouristWalletDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.AdventureCoins.ShouldBe(5);

        }


        private static TouristWalletController CreateController(IServiceScope scope)
        {
            return new TouristWalletController(scope.ServiceProvider.GetRequiredService<ITouristWalletService>());
        }
    }
}
