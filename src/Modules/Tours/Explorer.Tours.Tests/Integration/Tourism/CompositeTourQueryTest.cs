using Explorer.API.Controllers.Author.Administration;
using Explorer.API.Controllers.Tourist.Tour;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourism;

[Collection("Sequential")]
public class CompositeTourQueryTest : BaseToursIntegrationTest
{
    public CompositeTourQueryTest(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // ActD
        var result = ((ObjectResult)controller.GetAllComposite(0, 0).Result)?.Value as PagedResult<CompositeTourDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(2);
        result.TotalCount.ShouldBe(2);
    }

    private static CompositeTourController CreateController(IServiceScope scope)
    {
        return new CompositeTourController(scope.ServiceProvider.GetRequiredService<ICompositeTourService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}

