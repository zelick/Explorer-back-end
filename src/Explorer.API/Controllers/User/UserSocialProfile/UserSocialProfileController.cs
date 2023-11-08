using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.ProfileMessaging
{
    //[Authorize(Policy = "userPolicy")]
    [Route("api/profile-messaging")]
    public class UserSocialProfileController : BaseApiController
    {
        private readonly IUserProfileService _userProfileService;
        

        public UserSocialProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost("follow/{followersId}/{followedId}")]
        public ActionResult<UserProfileDto> Follow(int followersId, int followedId)
        {

            var result = _userProfileService.Follow(followersId, followedId);

            return CreateResponse(result);
        }

        [HttpGet("get-user-social-profile/{userId:int}")]
        public ActionResult<UserProfileDto> GetUserSocialProfile(int userId)
        {
            var socialProfile = _userProfileService.Get(userId);

            return CreateResponse(socialProfile);
        }

        [HttpPost("send-message")]
        public ActionResult<MessageDto> SendMessage([FromBody] MessageDto message)
        {
            //var result = _messageService.Create(message);

            var result = _userProfileService.SendMessage(message);

            return CreateResponse(result);
        }

        [HttpPut("mark-as-read/{messageId:int}")]
        public ActionResult<MessageDto> ReadMessage(int messageId)
        {
            var result = _userProfileService.MarkAsRead(messageId);

            return CreateResponse(result);
        }

        [HttpGet("get-notifications/{userId:int}")]
        public ActionResult<List<MessageDto>> GetNotifications(int userId)
        {
            var notifications = _userProfileService.GetNotifications(userId);

            return CreateResponse(notifications);
        }

    }
}
