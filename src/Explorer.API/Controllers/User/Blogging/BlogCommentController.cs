using Microsoft.AspNetCore.Mvc;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.User.Blogging;

[Authorize(Policy = "userPolicy")]
[ApiController]
[Route("api/blogging/blog-posts/{id:int}/comments")]
public class BlogCommentController : BaseApiController
{
    private readonly IBlogCommentService _blogCommentService;

    public BlogCommentController(IBlogCommentService blogCommentService)
    {
        _blogCommentService = blogCommentService;
    }

    [HttpPatch]
    public ActionResult<BlogPostDto> Add(int id, [FromBody] BlogCommentDto blogComment)
    {
        var result = _blogCommentService.Add(id, blogComment);
        return CreateResponse(result);
    }

    [HttpPatch("remove")]
    public ActionResult<BlogPostDto> Remove(int id, [FromBody] BlogCommentDto blogComment)
    {
        var result = _blogCommentService.Remove(id, blogComment);
        return CreateResponse(result);
    }
}