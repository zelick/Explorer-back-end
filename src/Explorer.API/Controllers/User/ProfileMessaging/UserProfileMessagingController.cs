using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.ProfileMessaging
{
    [Authorize(Policy = "userPolicy")]
    [Route("api/profile-messaging")]
    public class UserProfileMessagingController : BaseApiController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMessageService _messageService;

        public UserProfileMessagingController(IUserProfileService userProfileService, IMessageService messageService)
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
