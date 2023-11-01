using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/objectRequests")]
    public class ObjectRequestController : BaseApiController
    {

        private readonly IObjectRequestService _objectRequestService;

        public ObjectRequestController(IObjectRequestService objectRequestService)
        {
            _objectRequestService = objectRequestService;
        }

        [HttpPost]
        public ActionResult<ObjectRequestDto> Create([FromBody] ObjectRequestDto request)
        {
            var result = _objectRequestService.Create(request);
            return CreateResponse(result);
        }

    }
}
