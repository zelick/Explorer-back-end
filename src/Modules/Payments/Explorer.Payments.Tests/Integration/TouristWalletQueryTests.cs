using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration
{
    public class TouristWalletQueryTests : BasePaymentsIntegrationTest
    {
        public TouristWalletQueryTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Get_adventure_coins()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Act
            var result = ((ObjectResult)controller.GetAdventureCoins(-21).Result)?.Value as TouristWalletDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.AdventureCoins.ShouldBe(500);

        }


        private static TouristWalletController CreateController(IServiceScope scope)
        {
            return new TouristWalletController(scope.ServiceProvider.GetRequiredService<ITouristWalletService>());
        }
    }
}
