using Microsoft.AspNetCore.Mvc;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;

namespace Explorer.API.Controllers.User.Blogging;

[Authorize(Policy = "userPolicy")]
[ApiController]
[Route("api/blogging/blog-posts/{blogId:int}/comments")]
public class BlogCommentController : BaseApiController
{
    private readonly IBlogCommentService _blogCommentService;

    public BlogCommentController(IBlogCommentService blogCommentService)
    {
        _blogCommentService = blogCommentService;
    }

    [HttpPatch]
    public ActionResult<BlogPostDto> Add(int blogId, [FromBody] BlogCommentDto blogComment)
    {
        if (User.PersonId() != blogComment.UserId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

        var result = _blogCommentService.Add(blogId, blogComment);
        return CreateResponse(result);
    }

    [HttpPatch("remove")]
    public ActionResult<BlogPostDto> Remove(int blogId, [FromBody] BlogCommentDto blogComment)
    {
        var result = _blogCommentService.Remove(blogId, blogComment, User.PersonId());
        return CreateResponse(result);
    }
}