using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Authentication
{
    public class SecureTokenQueryTests : BaseStakeholdersIntegrationTest
    {
        public SecureTokenQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Get_secure_token_by_username()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetSecureToken("autor1@gmail.com").Result)?.Value as SecureTokenDto;

            // Assert
            result.ShouldNotBeNull();
            result.TokenData.ShouldBe("unusedTokenDataOfUser11");
        }

        [Fact]
        public void Get_secure_token_by_token_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetUserIdByTokenData("unusedTokenDataOfUser12").Result)?.Value;

            // Assert
            result.ShouldNotBe(0);
            result.ShouldBe(-12);
        }

        private static SecureTokenController CreateController(IServiceScope scope)
        {
            return new SecureTokenController(scope.ServiceProvider.GetRequiredService<ISecureTokenService>());
        }
    }
}
