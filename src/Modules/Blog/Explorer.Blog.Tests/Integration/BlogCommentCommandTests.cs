using Explorer.API.Controllers.User.Blogging;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Blog.Tests.Integration;

[Collection("Sequential")]
public class BlogCommentCommandTests : BaseBlogIntegrationTest
{
    public BlogCommentCommandTests(BlogTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        var newBlogComment = new BlogCommentDto()
        {
            UserId = -1,
            CreationTime = DateTime.MinValue,
            Text = "Blog comment text."
        };

        // Act
        var result = ((ObjectResult)controller.Add(-1, newBlogComment).Result)?.Value as BlogPostDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Comments?[0].Text.ShouldBe(newBlogComment.Text);

        // Assert - Database
        var storedEntity = dbContext.BlogPosts.FirstOrDefault(i => i.Id == result.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var newBlogComment = new BlogCommentDto
        {
            UserId = 1,
            CreationTime = DateTime.Parse("2023-02-17 06:30:00"),
            ModificationTime = DateTime.MaxValue,
            Text = "Updated blog comment text."
        };

        // Act
        var result = (ObjectResult)controller.Add(-42, newBlogComment).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        var updatedBlogComment = new BlogCommentDto
        {
            UserId = -1,
            CreationTime = DateTime.Parse("2023-02-17 06:30:00"),
            ModificationTime = DateTime.Today,
            Text = "Updated blog comment text."
        };

        // Act
        var result = ((ObjectResult)controller.Add(-11, updatedBlogComment).Result)?.Value as BlogPostDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-11);
        result.Comments?[0].Text.ShouldBe(updatedBlogComment.Text);
        result.Comments?[0].ModificationTime?.Date.ShouldBe(updatedBlogComment.ModificationTime.Value.Date);
        result.Comments?[0].CreationTime.ShouldBe(updatedBlogComment.CreationTime);
        result.Comments?[0].UserId.ShouldBe(updatedBlogComment.UserId);

        // Assert - Database
        var storedEntity = dbContext.BlogPosts.FirstOrDefault(i => i.Id == result.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.Comments?[0].Text.ShouldBe(updatedBlogComment.Text);
        storedEntity.Comments?[0].CreationTime.ShouldBe(updatedBlogComment.CreationTime);
        storedEntity.Comments?[0].ModificationTime?.Date.ShouldBe(updatedBlogComment.ModificationTime.Value.Date);
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedBlogComment = new BlogCommentDto
        {
            Text = "Updated blog comment text."
        };

        // Act
        var result = (ObjectResult)controller.Add(-42, updatedBlogComment).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        var blogCommentDto = new BlogCommentDto
        {
            UserId = -12,
            CreationTime = DateTime.Parse("2023-02-17 06:30:00"),
            ModificationTime = DateTime.MaxValue,
            Text = "Updated blog comment text."
        };

        // Act
        var result = controller.Remove(-12, blogCommentDto).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.ShouldBeOfType<OkResult>(); ;

        // Assert - Database
        var storedBlogPost = dbContext.BlogPosts.FirstOrDefault(i => i.Id == -12);
        storedBlogPost.Comments.Count.ShouldBe(1);
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        var blogCommentDto = new BlogCommentDto
        {
            UserId = -42,
            CreationTime = DateTime.MinValue,
            ModificationTime = DateTime.MaxValue,
            Text = "Updated blog comment text."
        };

        // Act
        var result = (ObjectResult)controller.Remove(-1, blogCommentDto).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    private static BlogCommentController CreateController(IServiceScope scope)
    {
        return new BlogCommentController(scope.ServiceProvider.GetRequiredService<IBlogCommentService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}