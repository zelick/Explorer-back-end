using Explorer.API.Controllers.Author.Administration;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Encounters.Tests.Integration.Administration;
[Collection("Sequential")]
public class EncounterCommandTests : BaseEncountersIntegrationTest
{
    public EncounterCommandTests(EncountersTestFactory factory) : base(factory) { }
    
    [Fact]
    public void CreateMiscEncounterSuccessfully()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 200;
        var  checkpointId = -1;
        bool isSecretPrerequisite = true;

        var newEntity = new EncounterDto
        {
            AuthorId=-12,
            Name = "Izazov misc",
            Description = "Potrebno je uraditi 20 sklekova.",
            XP = 2,
            EncounterType="Misc",
            Longitude=45,
            Latitude=45
        };

        // Act
        var result = (ObjectResult)controller.Create(newEntity,checkpointId, isSecretPrerequisite).Result;

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
    }

    [Fact]
    public void CreateHiddenLocationEncounterSuccessfully()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 200;
        var checkpointId = -2;
        bool isSecretPrerequisite = true;


        var newValueObject = new HiddenLocationEncounterDto
        {
            Longitude = 45,
            Latitude = 45,
            Image="slika.jpg",
            Range=15
        };

        var newEntity = new EncounterDto
        {
            AuthorId = -12,
            Name = "Izazov hidden location",
            Description = "Pronadji gde je nastala slika.",
            XP = 2,
            EncounterType = "Location",
            Longitude = 45,
            Latitude = 45,
            HiddenLocationEncounter = newValueObject

        };

        // Act
        var result = (ObjectResult)controller.Create(newEntity, checkpointId,isSecretPrerequisite).Result;

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);


    }

    [Fact]
    public void CreateSocialEncounterSuccessfully()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 200;
        var checkpointId = -3;
        bool isSecretPrerequisite = false;

        var newValueObject = new SocialEncounterDto
        {
            Range = 15,
            RequiredPeople=2
        };

        var newEntity = new EncounterDto
        {
            AuthorId = -12,
            Name = "Social izazov",
            Description = "Potrebno je zajedno sa ostalim clanovima uraditi izazov.",
            XP = 2,
            EncounterType = "Social",
            Longitude = 45,
            Latitude = 45,
            SocialEncounter = newValueObject

        };

        // Act
        var result = (ObjectResult)controller.Create(newEntity, checkpointId,isSecretPrerequisite).Result;

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
    }
    private static EncounterController CreateController(IServiceScope scope)
    {
        return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
        {
            ControllerContext = BuildContext("-12")
        };
    }
}

