using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
	//[Authorize(Policy = "touristPolicy")]
	[Route("api/user-club")]
	public class UserClubController : BaseApiController
	{
		private readonly IUserClubService _userClubService;

		public UserClubController(IUserClubService userClubService)
		{
			_userClubService = userClubService;
		}

		[HttpGet]
		public ActionResult<PagedResult<UserClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = _userClubService.GetPaged(page, pageSize);
			return CreateResponse(result);
		}

		[HttpPost]
		public ActionResult<UserClubDto> Create([FromBody] UserClubDto userClub)
		{
			var result = _userClubService.Create(userClub);
			return CreateResponse(result);
		}

		[HttpPut("{id:int}")]
		public ActionResult<UserClubDto> Update([FromBody] UserClubDto userClub)
		{
			var result = _userClubService.Update(userClub);
			return CreateResponse(result);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			var result = _userClubService.Delete(id);
			return CreateResponse(result);
		}

		/*HttpGet("/usersForClub/{id:int}")]
		public ActionResult<PagedResult<UserClubDto>> GetUsersForClub(int id)
		{
			var result = _userClubService.GetPaged(id);
			return CreateResponse(result);
		}*/

	}
}
