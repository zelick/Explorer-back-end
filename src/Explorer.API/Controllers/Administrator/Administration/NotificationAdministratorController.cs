using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorAndAuthorPolicy")]
    [Route("api/administrator/notifications")]
    public class NotificationAdministratorController : BaseApiController
    {
        private readonly INotificationService _service;

        public NotificationAdministratorController(INotificationService notificationService)
        {
            _service = notificationService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<NotificationDto> Get(int id)
        {
            var result = _service.Get(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<NotificationDto> Update([FromBody] NotificationDto notification)
        {
            var result = _service.Update(notification);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("get-all/{id:int}")]
        public ActionResult<PagedResult<NotificationDto>> GetAllByUser(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _service.GetAllByUser(id, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("get-unread/{id:int}")]
        public ActionResult<PagedResult<NotificationDto>> GetUnreadByUser(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _service.GetUnreadByUser(id, page, pageSize);
            return CreateResponse(result);
        }
    }
}
