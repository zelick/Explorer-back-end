using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/manipulation/equipment")]
    public class EquipmentController : BaseApiController
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost("get-available")]
        public ActionResult<List<EquipmentDto>> GetAvailableEquipment([FromBody]List<long> currentEquipmentIds)
        {
            var result = _equipmentService.GetAvailable(currentEquipmentIds);
            return CreateResponse(result);
        }


    }
}
