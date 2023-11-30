using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    public class PurchasedTourQueryTests : BasePaymentsIntegrationTest
    {
        public PurchasedTourQueryTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all_by_user()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        
            var touristId = -21;
        
            // Act
            var result = (ObjectResult)controller.GetUsersPurchasedTours(touristId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }
        
        
        [Fact]
        public void Retrieves_details_by_user()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        
            var tourId = -4;
        
            // Act
            var result = (ObjectResult)controller.GetUsersPurchasedTourDetails(tourId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public void Retrieves_details_by_user_fails_doesnt_own()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            var tourId = -1;

            // Act
            var result = (ObjectResult)controller.GetUsersPurchasedTourDetails(tourId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(403);
        }

        private static PurchasedTourController CreateController(IServiceScope scope)
        {
            return new PurchasedTourController(scope.ServiceProvider.GetRequiredService<IItemOwnershipService>(), 
                scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-21")
            };
        }
    }
}