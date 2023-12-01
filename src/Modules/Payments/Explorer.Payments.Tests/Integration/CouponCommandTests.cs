using Explorer.API.Controllers.Author.Administration;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.Core.Domain.Tours;
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
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newCoupon = new CreateCouponDto
            {
                DiscountPercentage = 25,
                ExpirationDate = DateTime.UtcNow.AddYears(1),
                IsGlobal = true,
                TourId = null
            };

            // Act
            var result = ((ObjectResult)controller.Create(newCoupon).Result)?.Value as CouponDto;

            // Assert
            result.ShouldNotBeNull();
            result.DiscountPercentage.ShouldBe(newCoupon.DiscountPercentage);
            result.ExpirationDate.ShouldBe(newCoupon.ExpirationDate);
            result.IsGlobal.ShouldBe(newCoupon.IsGlobal);
            result.TourId.ShouldBe(newCoupon.TourId);

            var storedEntity = dbContext.Coupons.OrderBy(i => i.Id).LastOrDefault(c => c.Id == result.Id);

            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newCoupon = new CreateCouponDto
            {
                DiscountPercentage = 25,
                ExpirationDate = DateTime.UtcNow.AddYears(-1),
                IsGlobal = true,
                TourId = -2
            };

            // Act
            var result = (ObjectResult)controller.Create(newCoupon).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var updatedCoupon = new CouponDto
            {
                Id = -1,
                Code = "EEE1G3PS",
                DiscountPercentage = 50,
                ExpirationDate = DateTime.UtcNow.AddYears(2),
                IsGlobal = true,
                TourId = null
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedCoupon).Result).Value as CouponDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Code.ShouldBe(updatedCoupon.Code);
            result.DiscountPercentage.ShouldBe(updatedCoupon.DiscountPercentage);
            result.ExpirationDate.ShouldBe(updatedCoupon.ExpirationDate);
            result.IsGlobal.ShouldBe(updatedCoupon.IsGlobal);
            result.TourId.ShouldBe(updatedCoupon.TourId);

            var storedEntity = dbContext.Coupons.OrderBy(i => i.Id).FirstOrDefault(c => c.Id == result.Id);

            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedCoupon = new CouponDto
            {
                Id = -101,
                Code = "EEE1G3PS",
                DiscountPercentage = 50,
                ExpirationDate = DateTime.UtcNow.AddYears(2),
                IsGlobal = true,
                TourId = null
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedCoupon).Result;

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
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var deletedCouponId = -3;

            // Act
            var result = (OkResult)controller.Delete(deletedCouponId);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            var storedCoupon = dbContext.Coupons.FirstOrDefault(c => c.Id == deletedCouponId);
            storedCoupon.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var deletedCouponId = -101;

            // Act
            var result = (ObjectResult)controller.Delete(-101);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static CouponController CreateController(IServiceScope scope)
        {
            return new CouponController(scope.ServiceProvider.GetRequiredService<ICouponService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
