using Explorer.API.Controllers.User.Blogging;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Blog.Tests.Integration;

[Collection("Sequential")]
public class BlogPostQueryTests : BaseBlogIntegrationTest
{
    public BlogPostQueryTests(BlogTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<BlogPostDto>;

        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(4);
        result.TotalCount.ShouldBe(4);
    }

    [Fact]
    public void Retrieves_by_user()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var result = ((ObjectResult)controller.GetByUser(0, 0, -1).Result)?.Value as PagedResult<BlogPostDto>;

        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }

    private static BlogPostController CreateController(IServiceScope scope)
    {
        return new BlogPostController(scope.ServiceProvider.GetRequiredService<IBlogPostService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}