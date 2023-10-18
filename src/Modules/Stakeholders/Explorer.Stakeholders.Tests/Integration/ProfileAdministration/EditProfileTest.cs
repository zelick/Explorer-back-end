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
    public class EditProfileTest : BaseStakeholdersIntegrationTest
    {
        public EditProfileTest(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Successfully_edited_user()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var accountForUpdate = new PersonDto
            {
                Id = -11,
                UserId = -11,
                Email = "novimejl@gmail.com",
                Name = "Ana",
                Surname = "Nikolic",
                Biography = "editovana bio",
                ProfilePictureUrl = "editovanaslika.jpg",
                Motto = "novi moto",
            };

            // Act
            var authenticationResponse = ((ObjectResult)controller.Edit(accountForUpdate).Result).Value as PersonDto;

            // Assert - Response
            authenticationResponse.ShouldNotBeNull();
            authenticationResponse.Email.ShouldBe(accountForUpdate.Email);
            authenticationResponse.Name.ShouldBe(accountForUpdate.Name);
            authenticationResponse.Surname.ShouldBe(accountForUpdate.Surname);
            authenticationResponse.Biography.ShouldBe(accountForUpdate.Biography);
            authenticationResponse.ProfilePictureUrl.ShouldBe(accountForUpdate.ProfilePictureUrl);
            authenticationResponse.Motto.ShouldBe(accountForUpdate.Motto);

            //Assert - Database
            var storedEntity = dbContext.People.FirstOrDefault(i => i.ProfilePictureUrl == "editovanaslika.jpg");
            storedEntity.ShouldNotBeNull();
            storedEntity.Biography.ShouldBe(authenticationResponse.Biography);  
            var oldEntity = dbContext.People.FirstOrDefault(i => i.Biography == "prva bio");
            oldEntity.ShouldBeNull();
        }

        private static ProfileEditingController CreateController(IServiceScope scope)
        {
            return new ProfileEditingController(scope.ServiceProvider.GetRequiredService<IPersonEditingService>());
        }
    }
}
