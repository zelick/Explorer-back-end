using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.ProfileMessaging
{
    [Authorize(Policy = "userPolicy")]
    [Route("api/profile-messaging")]
    public class UserSocialProfileController : BaseApiController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMessageService _messageService;
        

        public UserSocialProfileController(IUserProfileService userProfileService, IMessageService messageService)
        {
            _userProfileService = userProfileService;
            _messageService = messageService;
        }

        [HttpPost("follow/{userId}/{followedUserId}")]
        public ActionResult Follow(int userId, int followedUserId)
        {

            _userProfileService.Follow(userId, followedUserId);

            return null;
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
            var result = _messageService.Create(message);

            return CreateResponse(result);
        }

        [HttpPut("mark-as-read/{messageId:int}")]
        public ActionResult<MessageDto> ReadMessage(int messageId)
        {
            var result = _messageService.MarkAsRead(messageId);

            return CreateResponse(result);
        }

        [HttpGet("get-notifications/{userId:int}")]
        public ActionResult<List<MessageDto>> GetNotifications(int userId)
        {
            var notifications = _messageService.GetNotifications(userId);

            return CreateResponse(notifications);
        }

    }
}
