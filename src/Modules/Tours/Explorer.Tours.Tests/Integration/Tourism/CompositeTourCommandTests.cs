using Explorer.API.Controllers.Author.Administration;
using Explorer.API.Controllers.Tourist.Tour;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Explorer.Tours.Tests.Integration.Tourism;

[Collection("Sequential")]

public class CompositeTourCommandTests : BaseToursIntegrationTest
{
    public CompositeTourCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new CompositeTourCreationDto
        {
            Id = 1,
            OwnerId = -23,
            Name = "Example Tour",
            Description = "This is an example tour description.",
            TourIds = new List<long> { -1, -5 }
        };

        // Act
        var result = ((ObjectResult)controller.CreateComposite(newEntity).Result)?.Value as CompositeTourCreationDto;

        // Assert - Response
        result.ShouldNotBeNull();


        // Assert - Database
        var storedEntity = dbContext.CompositeTours.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var newEntity = new CompositeTourCreationDto
        {
            Id = 1,
            OwnerId = -23,
            Name = "Example Tour",
            Description = "This is an example tour description.",
            TourIds = new List<long> { }
        };

        // Act
        var result = (ObjectResult)controller.CreateComposite(newEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.DeleteComposite(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.CompositeTours.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.DeleteComposite(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static CompositeTourController CreateController(IServiceScope scope)
    {
        return new CompositeTourController(scope.ServiceProvider.GetRequiredService<ICompositeTourService>())
        {
            ControllerContext = BuildContext("-12")
        };
    }
}

