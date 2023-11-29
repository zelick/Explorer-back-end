using Explorer.API.Services;
using Explorer.Blog.Core.Domain.BlogPosts;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
	[Authorize(Policy = "authorPolicy")]
	[Route("api/administration/tour-bundle")]
	public class TourBundleController : BaseApiController
	{
		public TourBundleController() { }

		[HttpPost]
		public ActionResult<TourBundleDto> Create([FromBody] TourBundleDto tourBundle)
		{
			throw new NotImplementedException();
		}
	}
}
