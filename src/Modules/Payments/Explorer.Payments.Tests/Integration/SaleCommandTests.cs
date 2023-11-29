using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Tests;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class SaleTests : BasePaymentsIntegrationTest
    {
        public SaleTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            var controller = CreateController();
            var newEntity = new SaleDto();

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as SaleDto;

            // Assert - Response
            result.ShouldNotBeNull();
        }

        private static SaleController CreateController()
        {
            return new SaleController()
            {
                ControllerContext = BuildContext("-12")
            };
        }
    }
}