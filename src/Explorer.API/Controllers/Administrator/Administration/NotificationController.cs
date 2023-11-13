using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorAndAuthorPolicy")]
    [Route("api/administration/notification")]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public ActionResult<NotificationDto> AddNotification([FromBody] NotificationDto notificationDto)
        {
            var result = _notificationService.AddNotification(notificationDto);
            return CreateResponse(result);
        }

        [HttpGet("getAllUnread/{userId:int}")]
        public ActionResult<List<NotificationDto>> GetAllUnread(int userId)
        {
            var result = _notificationService.GetAllUnread(userId);
            return CreateResponse(result);
        }

        [HttpPut("markAsRead/{id:int}")]
        public ActionResult<NotificationDto> MarkAsRead(int id)
        {
            var result = _notificationService.MarkAsRead(id);
            return CreateResponse(result);
        }
    }
}
