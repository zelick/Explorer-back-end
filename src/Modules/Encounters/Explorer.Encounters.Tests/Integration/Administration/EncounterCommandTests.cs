using Explorer.API.Controllers.Author.Administration;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Infrastructure.Database;
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
        var  checkpointId = -3;
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
        var updatedObject = result?.Value as EncounterDto;


        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

        updatedObject?.AuthorId.ShouldBe(-12);
        updatedObject?.Name.ShouldBe(newEntity.Name);
        updatedObject?.Description.ShouldBe(newEntity.Description);
        updatedObject?.XP.ShouldBe(2);
        updatedObject?.Longitude.ShouldBe(45);
        updatedObject?.Latitude.ShouldBe(45);
    }

    [Fact]
    public void CreateHiddenLocationEncounterSuccessfully()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 200;
        var checkpointId = -3;
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
        var updatedObject = result?.Value as EncounterDto;


        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
        updatedObject?.AuthorId.ShouldBe(-12);
        updatedObject?.Name.ShouldBe(newEntity.Name);
        updatedObject?.Description.ShouldBe(newEntity.Description);
        updatedObject?.XP.ShouldBe(newEntity.XP);
        updatedObject?.Longitude.ShouldBe(newEntity.Longitude);
        updatedObject?.Latitude.ShouldBe(newEntity.Latitude);
        updatedObject?.HiddenLocationEncounter?.Image.ShouldBe(newValueObject.Image);
        updatedObject?.HiddenLocationEncounter?.Range.ShouldBe(newValueObject.Range);
        updatedObject?.HiddenLocationEncounter?.Longitude.ShouldBe(newValueObject.Longitude);
        updatedObject?.HiddenLocationEncounter?.Latitude.ShouldBe(newValueObject.Latitude);


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
        var updatedObject = result?.Value as EncounterDto;


        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
        updatedObject?.AuthorId.ShouldBe(-12);
        updatedObject?.Name.ShouldBe(newEntity.Name);
        updatedObject?.Description.ShouldBe(newEntity.Description);
        updatedObject?.XP.ShouldBe(newEntity.XP);
        updatedObject?.Longitude.ShouldBe(newEntity.Longitude);
        updatedObject?.Latitude.ShouldBe(newEntity.Latitude);
    }

    [Fact]
    public void InvalidAuthor()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 403;
        var checkpointId = -6;
        bool isSecretPrerequisite = true;

        var newEntity = new EncounterDto
        {
            AuthorId = -22,
            Name = "Izazov misc",
            Description = "Potrebno je uraditi 20 sklekova.",
            XP = 2,
            EncounterType = "Misc",
            Longitude = 45,
            Latitude = 45
        };

        // Act
        var result = (ObjectResult)controller.Create(newEntity, checkpointId, isSecretPrerequisite).Result;

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
    }

    [Fact]
    public void InvalidCheckpoint()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 404;
        var checkpointId = -16;
        bool isSecretPrerequisite = true;

        var newEntity = new EncounterDto
        {
            AuthorId = -12,
            Name = "Izazov misc",
            Description = "Potrebno je uraditi 20 sklekova.",
            XP = 2,
            EncounterType = "Misc",
            Longitude = 45,
            Latitude = 45
        };

        // Act
        var result = (ObjectResult)controller.Create(newEntity, checkpointId, isSecretPrerequisite).Result;

        //Assert

        result.StatusCode.ShouldBe(expectedResponseCode);
    }

    [Fact]
    public void InvalidArgument()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 400;
        var checkpointId = -3;
        bool isSecretPrerequisite = true;

        var newEntity = new EncounterDto
        {
            AuthorId = -12,
            Name = "",
            Description = "Potrebno je uraditi 20 sklekova.",
            XP = -2,
            EncounterType = "Misc",
            Longitude = 45,
            Latitude = 45
        };

        // Act
        var result = (ObjectResult)controller.Create(newEntity, checkpointId, isSecretPrerequisite).Result;

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
    }

    [Fact]
    public void UpdateSuccessfully()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 200;

        var newEntity = new EncounterDto
        {
            Id = -1,
            AuthorId = -12,
            Name = "Misc Encounter",
            Description = "Potrebno je uraditi 80 sklekova.",
            XP = 20,
            EncounterType = "Misc",
            Longitude = 45,
            Latitude = 45,
        };

        // Act
        var result = (ObjectResult)controller.Update(newEntity).Result;
        var updatedObject = result?.Value as EncounterDto;


        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
        updatedObject.Id.ShouldBe(-1);
        updatedObject.AuthorId.ShouldBe(-12);
        updatedObject.Name.ShouldBe(updatedObject.Name);
        updatedObject.Description.ShouldBe(updatedObject.Description);
        updatedObject.XP.ShouldBe(updatedObject.XP);
        updatedObject.EncounterType.ShouldBe(updatedObject.EncounterType);
        updatedObject.Longitude.ShouldBe(updatedObject.Longitude);
        updatedObject.Latitude.ShouldBe(updatedObject.Latitude);
    }

    [Fact]
    public void InvalidAuthorOnUpdate()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 403;

        var newEntity = new EncounterDto
        {
            Id=-1,
            AuthorId = -16,
            Name = "Misc Encounter",
            Description = "Potrebno je uraditi 80 sklekova.",
            XP = 20,
            EncounterType = "Misc",
            Longitude = 45,
            Latitude = 45
        };

        // Act
        var result = (ObjectResult)controller.Update(newEntity).Result;

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);

    }

    [Fact]
    public void InvalidArgumentOnUpdate()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var expectedResponseCode = 400;

        var newEntity = new EncounterDto
        {
            Id=-1,
            AuthorId = -12,
            Name = "",
            Description = "Potrebno je uraditi 80 sklekova.",
            XP = -20,
            EncounterType = "Misc",
            Longitude = 45,
            Latitude = 45
        };

        // Act
        var result = (ObjectResult)controller.Update(newEntity).Result;
        var updatedObject = result?.Value as EncounterDto;

        //Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedResponseCode);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
        var checkpointId = -3;

        // Act
        var result = (OkResult)controller.Delete(checkpointId);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var checkpointId = -1000;

        // Act
        var result = (ObjectResult)controller.Delete(checkpointId);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }


    private static EncounterController CreateController(IServiceScope scope)
    {
        return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
        {
            ControllerContext = BuildContext("-12")
        };
    }
}

