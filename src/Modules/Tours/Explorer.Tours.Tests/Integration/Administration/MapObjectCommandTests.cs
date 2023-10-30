using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class MapObjectCommandTests : BaseToursIntegrationTest
{
    public MapObjectCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateMapObjectController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newMapObject = new MapObjectDto
        {
            Name = "Example MapObject",
            Description = "A description for the map object",
            PictureURL = "https://example.com/image.jpg",
            Category = "Other",
            Longitude = (float?)123.4,
            Latitude = (float?)78.9
        };

        // Act
        var result = ((ObjectResult)controller.Create(newMapObject).Result)?.Value as MapObjectDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newMapObject.Name);

        // Assert - Database
        var storedMapObject = dbContext.MapObjects.FirstOrDefault(i => i.Name == newMapObject.Name);
        storedMapObject.ShouldNotBeNull();
        storedMapObject.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateMapObjectController(scope);
        var invalidMapObject = new MapObjectDto
        {
            Name = ""
        };

        // Act
        var result = (ObjectResult)controller.Create(invalidMapObject).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateMapObjectController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedMapObject = new MapObjectDto
        {
            Id = -1,
            Name = "Updated MapObject",
            Description = "Updated description",
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedMapObject).Result)?.Value as MapObjectDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Name.ShouldBe(updatedMapObject.Name);
        result.Description.ShouldBe(updatedMapObject.Description);

        // Assert - Database
        var storedMapObject = dbContext.MapObjects.FirstOrDefault(i => i.Name == "Updated MapObject");
        storedMapObject.ShouldNotBeNull();
        storedMapObject.Description.ShouldBe(updatedMapObject.Description);
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateMapObjectController(scope);
        var invalidMapObject = new MapObjectDto
        {
            Id = -1000,
            Name = "Example MapObject",
            Description = "A description for the map object",
            PictureURL = "https://example.com/image.jpg",
            Category = "WC",
            Longitude = (float?)123.4,
            Latitude = (float?)78.9
        };

        // Act
        var result = (ObjectResult)controller.Update(invalidMapObject).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateMapObjectController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedMapObject = dbContext.MapObjects.FirstOrDefault(i => i.Id == -3);
        storedMapObject.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateMapObjectController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static MapObjectController CreateMapObjectController(IServiceScope scope)
    {
        return new MapObjectController(scope.ServiceProvider.GetRequiredService<IMapObjectService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
