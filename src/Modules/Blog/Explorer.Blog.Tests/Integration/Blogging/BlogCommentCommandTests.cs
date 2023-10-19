using Explorer.API.Controllers.User.Blogging;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Blog.Tests.Integration.Blogging;

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
            UserId = 1, 
            BlogPostId = 1,
            CreationTime = new DateTime(),
            ModificationTime = new DateTime(),
            Text = "Blog comment text."
        };

        // Act
        var result = ((ObjectResult)controller.Create(newBlogComment).Result)?.Value as BlogCommentDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Text.ShouldBe(newBlogComment.Text);

        // Assert - Database
        var storedEntity = dbContext.BlogComments.FirstOrDefault(i => i.Id == result.Id);
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
            BlogPostId = 1,
        };

        // Act
        var result = (ObjectResult)controller.Create(newBlogComment).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
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
            Id = -1,
            UserId = 0,
            BlogPostId = 1,
            CreationTime = new DateTime(),
            ModificationTime = new DateTime(),
            Text = "Updated blog comment text."
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedBlogComment).Result)?.Value as BlogCommentDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Text.ShouldBe(updatedBlogComment.Text);
        result.ModificationTime.ShouldBe(updatedBlogComment.ModificationTime);
        result.CreationTime.ShouldBe(updatedBlogComment.CreationTime);
        result.BlogPostId.ShouldBe(updatedBlogComment.BlogPostId);
        result.UserId.ShouldBe(updatedBlogComment.UserId);

        // Assert - Database
        var storedEntity = dbContext.BlogComments.FirstOrDefault(i => i.Text == "Updated blog comment text.");
        storedEntity.ShouldNotBeNull();
        storedEntity.Text.ShouldBe(updatedBlogComment.Text);
        storedEntity.CreationTime.ShouldBe(updatedBlogComment.CreationTime);
        storedEntity.ModificationTime.ShouldBe(updatedBlogComment.ModificationTime);
        var oldEntity = dbContext.BlogComments.FirstOrDefault(i => i.Text == "First blog comment.");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedBlogComment = new BlogCommentDto
        {
            Id = -1000,
            Text = "Updated blog comment text."
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedBlogComment).Result;

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

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.BlogComments.FirstOrDefault(i => i.Id == 1);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static BlogCommentController CreateController(IServiceScope scope)
    {
        return new BlogCommentController(scope.ServiceProvider.GetRequiredService<IBlogCommentService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}