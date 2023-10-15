using Microsoft.AspNetCore.Mvc;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.User.Blogging;

[Authorize(Policy = "userPolicy")]
[ApiController]
[Route("api/blogging/blog-comment")]
public class BlogCommentController : BaseApiController
{
    private readonly IBlogCommentService _blogCommentService;

    public BlogCommentController(IBlogCommentService blogCommentService)
    {
        _blogCommentService = blogCommentService;
    }

    [HttpPost]
    public ActionResult<BlogCommentDto> Create([FromBody] BlogCommentDto blogComment)
    {
        var result = _blogCommentService.Create(blogComment);
        return CreateResponse(result);
    }
}