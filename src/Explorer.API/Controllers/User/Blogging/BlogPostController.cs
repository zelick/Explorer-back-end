using Explorer.API.Services;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.Blogging;

[Authorize(Policy = "userPolicy")]
[Route("api/blogging/blog-posts")]
public class BlogPostController : BaseApiController
{
    private readonly IBlogPostService _blogPostService;
    private readonly ImageService _imageService;

    public BlogPostController(IBlogPostService blogPostService)
    {
        _blogPostService = blogPostService;
        _imageService = new ImageService();
    }

    [HttpGet]
    public ActionResult<PagedResult<BlogPostDto>> GetAllNonDraft([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? status = null)
    { 
        var result = status is null
            ? _blogPostService.GetAllNonDraft(page, pageSize)
            : _blogPostService.GetFilteredByStatus(page, pageSize, status);
        return CreateResponse(result);
    }

    [HttpGet("{id:int}")]
    public ActionResult<BlogPostDto> GetById(int id)
    {
        var result = _blogPostService.Get(id);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<BlogPostDto> Create([FromForm] BlogPostDto blogPost, List<IFormFile>? images)
    {
        if (images != null && images.Any())
        {
            var imageUrls = _imageService.UploadImages(images);
            blogPost.ImageUrls = imageUrls;
        }

        var result = _blogPostService.Create(blogPost);
        return CreateResponse(result);
    }

    // TODO authorization
    [HttpGet("user/{id:int}")]
    public ActionResult<PagedResult<BlogPostDto>> GetAllByUser([FromQuery] int page, [FromQuery] int pageSize, int id)
    {

        var result = _blogPostService.GetAllByUser(page, pageSize, id);
        return CreateResponse(result);
    }

    [HttpPut("{id:int}")]
    public ActionResult<BlogPostDto> Update([FromBody] BlogPostDto blogPost)
    {
        var result = _blogPostService.Update(blogPost);
        return CreateResponse(result);
    }

    [HttpPatch("{id:int}/close")]
    public ActionResult<BlogPostDto> Close(int id)
    {
        var result = _blogPostService.Close(id);
        return CreateResponse(result);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = _blogPostService.Delete(id);
        return CreateResponse(result);
    }

    [HttpPut("{id:int}/ratings")]
    public ActionResult<BlogPostDto> Rate(int id, [FromBody] BlogRatingDto blogRating)
    {
        var result = _blogPostService.Rate(id, blogRating);
        return CreateResponse(result);
    }
}