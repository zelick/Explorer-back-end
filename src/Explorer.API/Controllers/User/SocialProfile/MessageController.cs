using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.SocialProfile
{
    [Authorize(Policy = "userPolicy")]
    [Route("api/profile-messaging")]
    public class MessageController : BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("send")]
        public ActionResult<MessageDto> SendMessage(MessageDto messageDto)
        {
            var message = _messageService.Send(messageDto);

            return CreateResponse(message);
        }

        [HttpPut("update")]
        public ActionResult<MessageDto> Update(MessageDto messageDto)
        {
            var message = _messageService.Update(messageDto);
            return CreateResponse(message);
        }

        [HttpDelete("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _messageService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("sent/{userId:int}")]
        public ActionResult<List<MessageDto>> GetAllSent(int userId)
        {
            var sentMessages = _messageService.GetAllSent(userId);
            return CreateResponse(sentMessages);
        }

        [HttpGet("inbox/{userId:int}")]
        public ActionResult<List<MessageDto>> GetInbox(int userId)
        {
            var inbox = _messageService.GetAllReceived(userId);
            return CreateResponse(inbox);
        }

        [HttpGet("notifications/{userId:int}")]
        public ActionResult<List<MessageDto>> GetNotifications(int userId)
        {
            var notifications = _messageService.GetAllUnread(userId);
            return CreateResponse(notifications);
        }

        [HttpPut("read/{id:int}")]
        public ActionResult<MessageDto> Read(int id)
        {
            var result = _messageService.MarkAsRead(id);
            return CreateResponse(result);
        }

    }
}
