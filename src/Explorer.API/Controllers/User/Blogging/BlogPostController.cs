using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.Blogging;

[Authorize(Policy = "userPolicy")]
[Route("api/blogging/blog-post")]
public class BlogPostController : BaseApiController
{
    private readonly IBlogPostService _blogPostService;

    public BlogPostController(IBlogPostService blogPostService)
    {
        _blogPostService = blogPostService;
    }

    [HttpGet]
    public ActionResult<PagedResult<BlogPostDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _blogPostService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<BlogPostDto> Create([FromBody] BlogPostDto blogPost)
    {
        var result = _blogPostService.Create(blogPost);
        return CreateResponse(result);
    }
}