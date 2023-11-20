//using Explorer.BuildingBlocks.Core.UseCases;
//using Explorer.Tours.API.Dtos;
//using Explorer.Tours.API.Public.Administration;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Explorer.API.Controllers.Author.Administration
//{
//    [Authorize(Policy = "authorPolicy")]
//    [Route("api/author/notifications")]
//    public class ReportedIssueAuthorNotificationController : BaseApiController
//    {
//        private readonly IReportedIssueNotificationService _service;
//        public ReportedIssueAuthorNotificationController(IReportedIssueNotificationService service)
//        {
//            _service = service;
//        }

//        [HttpGet("{id:int}")]
//        public ActionResult<ReportedIssueNotificationDto> Get(int id)
//        {
//            var result = _service.Get(id);
//            return CreateResponse(result);
//        }

//        [HttpPut("{id:int}")]
//        public ActionResult<ReportedIssueNotificationDto> Update([FromBody] ReportedIssueNotificationDto notification)
//        {
//            var result = _service.Update(notification);
//            return CreateResponse(result);
//        }

//        [HttpDelete("{id:int}")]
//        public ActionResult Delete(int id)
//        {
//            var result = _service.Delete(id);
//            return CreateResponse(result);
//        }

//        [HttpGet("get-all/{id:int}")]
//        public ActionResult<PagedResult<ReportedIssueNotificationDto>> GetAllByUser(int id, [FromQuery] int page, [FromQuery] int pageSize)
//        {
//            var result = _service.GetAllByUser(id, page, pageSize);
//            return CreateResponse(result);
//        }

//        [HttpGet("get-unread/{id:int}")]
//        public ActionResult<PagedResult<ReportedIssueNotificationDto>> GetUnreadByUser(int id, [FromQuery] int page, [FromQuery] int pageSize)
//        {
//            var result = _service.GetUnreadByUser(id, page, pageSize);
//            return CreateResponse(result);
//        }
//    }
//}