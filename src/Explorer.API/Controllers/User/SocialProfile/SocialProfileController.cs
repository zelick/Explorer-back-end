using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.SocialProfile
{
    //[Authorize(Policy = "userPolicy")]
    [Route("api/profile-messaging")]
    public class SocialProfileController : BaseApiController
    {
        private readonly ISocialProfileService _userProfileService;
        

        public SocialProfileController(ISocialProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost("follow/{followersId}/{followedId}")]
        public ActionResult<SocialProfileDto> Follow(int followersId, int followedId)
        {

            var result = _userProfileService.Follow(followersId, followedId);

            return CreateResponse(result);
        }

        [HttpGet("get-user-social-profile/{userId:int}")]
        public ActionResult<SocialProfileDto> GetUserSocialProfile(int userId)
        {
            var socialProfile = _userProfileService.Get(userId);

            return CreateResponse(socialProfile);
        }
    }
}
