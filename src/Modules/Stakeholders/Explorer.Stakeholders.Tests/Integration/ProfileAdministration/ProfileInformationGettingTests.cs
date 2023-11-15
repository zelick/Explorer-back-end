using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.API.Controllers.User.ProfileAdministration;

namespace Explorer.Stakeholders.Tests.Integration.ProfileAdministration
{
    [Collection("Sequential")]
    public class ProfileInformationGettingTests : BaseStakeholdersIntegrationTest
    {
        public ProfileInformationGettingTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Successfully_getting_user()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var accountId = -11;

            // Act
            var authenticationResponse = ((ObjectResult)controller.GetUserInfo(accountId).Result).Value as PersonDto;

            // Assert - Response
            authenticationResponse.ShouldNotBeNull();
            authenticationResponse.Id.ShouldBe(accountId);

            //Assert - Database
            var storedEntity = dbContext.People.FirstOrDefault(i => i.Id == -11);
            storedEntity.ShouldNotBeNull();
            var wrongEntity = dbContext.People.FirstOrDefault(i => i.Id == 54);
            wrongEntity.ShouldBeNull();
        }

        private static ProfileEditingController CreateController(IServiceScope scope)
        {
            return new ProfileEditingController(scope.ServiceProvider.GetRequiredService<IPersonEditingService>());
        }
    }
}