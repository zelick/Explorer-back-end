using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/langing-page")]
    public class LandingPageController : BaseApiController
    {
        private readonly IBlogPostService _blogPostService;
        private readonly ITourService _tourService;

        
        public LandingPageController(IBlogPostService blogPostService, ITourService tourService)
        {
            _blogPostService = blogPostService;
            _tourService = tourService;
        }

        [HttpGet("top-rated-blogs/{count}")]
        public ActionResult<PagedResult<BlogPostDto>> GetTopRatedBlogPosts(int count)
        {
            var result = _blogPostService.GetTopRatedBlogPosts(count);
            return CreateResponse(result);
        }

        [HttpGet("top-rated-tours/{count}")]
        public ActionResult<PagedResult<TourDto>> GetTopRatedTours(int count)
        {
            var result = _tourService.GetTopRatedTours(count);
            return CreateResponse(result);
        }


    }
}
