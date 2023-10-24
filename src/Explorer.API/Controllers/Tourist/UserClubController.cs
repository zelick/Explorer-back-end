using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
	[Authorize(Policy = "touristPolicy")]
	[Route("api/user-club")]
	public class UserClubController : BaseApiController
	{
		private readonly IClubService _clubService;

		public UserClubController(IClubService clubService)
		{
			_clubService = clubService;
		}

		[HttpPut("/remove-from/{clubId:int}/{memberId:int}")]
		public ActionResult<ClubDto> RemoveMemberFromClub(int clubId, int memberId)
		{
			var result = _clubService.RemoveMember(memberId, clubId);
			return CreateResponse(result);
		}

		[HttpPut("/add-to/{clubId:int}/{memberId:int}")]
		public ActionResult<ClubDto> AddMemberToClub(int clubId, int memberId)
		{
			var result = _clubService.AddMember(memberId, clubId);
			return CreateResponse(result);
		}

		/*[HttpGet]
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

		[HttpDelete("/removeMemberFromClub/{memberId:int}/{clubId:int}")]
		public ActionResult DeleteByIds(int memberId, int clubId)
		{
			var result = _userClubService.DeleteByIds(memberId, clubId);
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
