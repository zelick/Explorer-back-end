using Explorer.API.Controllers.Author.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class TourBundleQueryTests : BaseToursIntegrationTest
{
    public TourBundleQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all_published()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        // Act
        var result = ((ObjectResult)controller.GetAllPublished(0, 0).Result)?.Value as PagedResult<TourBundleDto>;
        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(1);
    }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourBundleDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }

    [Fact]
    public void Retrieves_all_by_author()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAllByAuthor(0, 0).Result)?.Value as List<TourBundleDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(3);
    }
    [Fact]
    public void Retrieves_one()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetById(-2).Result)?.Value as TourBundleDto;

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe("Paket tura 2");
        result.AuthorId.ShouldBe(-11);
    }


    private static TourBundleController CreateController(IServiceScope scope)
    {
        return new TourBundleController(scope.ServiceProvider.GetRequiredService<ITourBundleService>())
        {
            ControllerContext = BuildContext("-11")
        };
    }
}