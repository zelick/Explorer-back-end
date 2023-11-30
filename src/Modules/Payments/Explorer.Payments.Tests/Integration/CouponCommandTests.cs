using Explorer.API.Controllers.Author.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class CouponCommandTests : BasePaymentsIntegrationTest
    {
        public CouponCommandTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var coupon = new Object();

            // Act
            var result = ((ObjectResult)controller.Create(coupon).Result).Value as Object;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(200);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var coupon = new Object();

            // Act
            var result = ((ObjectResult)controller.Create(coupon).Result).Value as Object;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedCoupon = new Object();

            // Act
            var result = ((ObjectResult)controller.Update(updatedCoupon).Result).Value as Object;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(200);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedCoupon = new Object();

            // Act
            var result = ((ObjectResult)controller.Update(updatedCoupon).Result).Value as Object;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(400);
        }


        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var deletedCouponId = -1;

            // Act
            var result = ((ObjectResult)controller.Delete(deletedCouponId)).Value;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(200);
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var deletedCouponId = -1;

            // Act
            var result = ((ObjectResult)controller.Delete(deletedCouponId)).Value;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(404);
        }

        private static CouponController CreateController(IServiceScope scope)
        {
            return new CouponController(/*scope.ServiceProvider.GetRequiredService<ICouponService>()*/)
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
