using Explorer.API.Services;
using Explorer.Blog.Core.Domain.BlogPosts;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
	//[Authorize(Policy = "authorPolicy")]
	[Route("api/administration/tour-bundle")]
	public class TourBundleController : BaseApiController
	{
		private readonly ITourBundleService _tourBundleService;

		public TourBundleController(ITourBundleService tourBundleService) 
		{
			_tourBundleService = tourBundleService;
		}

		[HttpPost]
		public ActionResult<TourBundleDto> Create([FromBody] TourBundleDto tourBundle)
		{
			var result = _tourBundleService.Create(tourBundle);
			return CreateResponse(result);
		}

		[HttpPut("{id:int}")]
		public ActionResult<ClubDto> Update([FromBody] TourBundleDto tourBundle)
		{
			var result = _tourBundleService.Update(tourBundle);
			return CreateResponse(result);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			var result = _tourBundleService.Delete(id);
			return CreateResponse(result);
		}
	}
}
