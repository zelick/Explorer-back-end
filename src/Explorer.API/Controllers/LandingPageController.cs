using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
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
        private readonly IApplicationGradeService _applicationGradeService;


        public LandingPageController(IBlogPostService blogPostService, ITourService tourService, IApplicationGradeService applicationGradeService)
        {
            _blogPostService = blogPostService;
            _tourService = tourService;
            _applicationGradeService = applicationGradeService;
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


        [HttpGet("get-all-tours-preview")]
        public ActionResult<PagedResult<TourPreviewDto>> GetAllToursPreview([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetFilteredPublishedTours(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("app-rating-exists/{id}")]
        public bool Exists(int id)
        {
            return _applicationGradeService.Exists(id);
        }
    }
}
