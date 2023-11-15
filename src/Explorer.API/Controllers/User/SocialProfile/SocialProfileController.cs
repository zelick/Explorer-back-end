using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.SocialProfile
{
    //[Authorize(Policy = "userPolicy")]
    [Route("api/social-profile")]
    public class SocialProfileController : BaseApiController
    {
        private readonly ISocialProfileService _userProfileService;
        

        public SocialProfileController(ISocialProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost("follow/{followerId:int}/{followedId:int}")]
        public ActionResult<SocialProfileDto> Follow(int followerId, int followedId)
        {

            var result = _userProfileService.Follow(followerId, followedId);

            return CreateResponse(result);
        }

        [HttpPost("un-follow/{followerId:int}/{unFollowedId:int}")]
        public ActionResult<SocialProfileDto> UnFollow(int followerId, int unFollowedId)
        {
            var result = _userProfileService.UnFollow(followerId, unFollowedId);

            return CreateResponse(result);
        }

        [HttpGet("get/{userId:int}")]
        public ActionResult<SocialProfileDto> GetSocialProfile(int userId)
        {
            var socialProfile = _userProfileService.Get(userId);

            return CreateResponse(socialProfile);
        }
    }
}
