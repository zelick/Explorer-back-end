using Microsoft.AspNetCore.Mvc;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Authorization;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using System.Security.Claims;

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

    [HttpGet]
    public ActionResult<PagedResult<BlogCommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _blogCommentService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<BlogCommentDto> Create([FromBody] BlogCommentDto blogComment)
    {
        var result = _blogCommentService.Create(blogComment);
        return CreateResponse(result);
    }

    [HttpPut("{id:int}")]
    public ActionResult<BlogCommentDto> Update([FromBody] BlogCommentDto equipment)
    {
        var result = _blogCommentService.Update(equipment);
        return CreateResponse(result);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        // var userId = long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var result = _blogCommentService.Delete(id);
        return CreateResponse(result);
    }
}