using Castle.Core.Resource;
using Explorer.API.Controllers.Tourist.Tour;
using Explorer.API.Controllers.User;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Shopping;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    public class CustomerCommandTests : BaseStakeholdersIntegrationTest
    {
        public CustomerCommandTests(StakeholdersTestFactory factory) : base(factory) { }
        [Fact]
        public void Customer_Create_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new CustomerDto
            {
                TouristId = -21,
                ShoppingCartId = -2
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CustomerDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TouristId.ShouldBe(newEntity.TouristId);
            result.TouristId.ShouldNotBe(0);

            result.ShoppingCartId.ShouldBe(newEntity.ShoppingCartId);
            result.ShoppingCartId.ShouldNotBe(0);

            // Assert - Database
            var storedEntity = dbContext.Customers.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void ShoppingCartCheckOut_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var customerId = -21;

            // Act
            var result = (ObjectResult)controller.ShoppingCartCheckOut(customerId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);       
        }

        [Fact]
        public void GetCustomersPurchasedToursIds_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var touristId = -21;

            // Act
            var result = (ObjectResult)controller.getCustomersPurchasedToursIds(touristId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public void GetCustomersPurchasedTours_Succeed()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var touristId = -21;

            // Act
            var result = (ObjectResult)controller.getCustomersPurchasedTours(touristId).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }
 

        [Fact]
        public void getCustomersPurchasedTourDetails_Succeed()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var tourId = -4;

            // Act
            var result = (ObjectResult)controller.getCustomersPurchasedTourDetails(tourId).Result;
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