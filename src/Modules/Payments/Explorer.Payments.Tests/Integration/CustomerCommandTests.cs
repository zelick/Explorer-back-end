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
    public class CustomerCommandTests : BasePaymentsIntegrationTest
    {
        public CustomerCommandTests(PaymentsTestFactory factory) : base(factory) { }
        [Fact]
        public void Customer_Create_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new CustomerDto
            {
                UserId = -21,
                ShoppingCartId = -3
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CustomerDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(newEntity.UserId);
            result.UserId.ShouldNotBe(0);

            result.ShoppingCartId.ShouldBe(newEntity.ShoppingCartId);
            result.ShoppingCartId.ShouldNotBe(0);

            // Assert - Database
            var storedEntity = dbContext.Customers.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void GetCustomersPurchasedToursIds_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        
            var touristId = -21;
        
            // Act
            var result = (ObjectResult)controller.GetUsersPurchasedToursIds(touristId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }
        
        [Fact]
        public void GetCustomersPurchasedTours_Succeed()
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
        public void getCustomersPurchasedTourDetails_Succeed()
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

        private static CustomerController CreateController(IServiceScope scope)
        {
            return new CustomerController(scope.ServiceProvider.GetRequiredService<ICustomerService>(), 
                scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}