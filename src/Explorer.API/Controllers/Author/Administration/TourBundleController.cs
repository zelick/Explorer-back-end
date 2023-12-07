using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/tour-bundle")]
	public class TourBundleController : BaseApiController
	{
		private readonly ITourBundleService _tourBundleService;

		public TourBundleController(ITourBundleService tourBundleService) 
		{
			_tourBundleService = tourBundleService;
		}

        [HttpGet("published")]
        [Authorize(Policy = "userPolicy")]
        public ActionResult<PagedResult<TourBundleDto>> GetAllPublished([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourBundleService.GetAllPublished(page, pageSize);
            return CreateResponse(result);
        }


        [HttpGet]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<PagedResult<TourBundleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = _tourBundleService.GetPaged(page, pageSize);
			return CreateResponse(result);
		}

		[HttpGet("bundles-by-author")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<List<TourBundleDto>> GetAllByAuthor([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = _tourBundleService.GetAllByAuthor(page, pageSize, User.PersonId());
			return CreateResponse(result);
		}

		[HttpPost]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<TourBundleDto> Create([FromBody] TourBundleDto tourBundle)
		{
			if (User.PersonId() != tourBundle.AuthorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));
			var result = _tourBundleService.Create(tourBundle);
			return CreateResponse(result);
		}

		[HttpPut("{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<TourBundleDto> Update([FromBody] TourBundleDto tourBundle)
		{
			if (User.PersonId() != tourBundle.AuthorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));
			var result = _tourBundleService.Update(tourBundle);
			return CreateResponse(result);
		}

		[HttpDelete("{id:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult Delete(int id)
		{
			var result = _tourBundleService.Delete(id);
			return CreateResponse(result);
		}

		[HttpGet("{id:int}")]
        [Authorize(Policy = "authorPolicy")]

        public ActionResult<TourBundleDto> GetById(int id)
		{
			var result = _tourBundleService.GetBundleById(id);
			return CreateResponse(result);
		}

		[HttpPut("remove-tour/{bundleId:int}/{tourId:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<TourBundleDto> RemoveTourFromBundle(int bundleId, int tourId)
		{
			var result = _tourBundleService.RemoveTourFromBundle(bundleId, tourId);
			return CreateResponse(result);
		}

		[HttpPut("add-tour/{bundleId:int}/{tourId:int}")]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<TourBundleDto> AddTourToBundle(int bundleId, int tourId)
		{
			var result = _tourBundleService.AddTourToBundle(bundleId, tourId);
			return CreateResponse(result);
		}
	}
}
