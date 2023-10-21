using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.API.Controllers.User.ProfileAdministration
{
    [Authorize(Policy = "userPolicy")]
    [Route("api/profile-administration/edit")]
    public class ProfileEditingController : BaseApiController
    {
        private readonly IPersonEditingService _personEditingService;

        public ProfileEditingController(IPersonEditingService personEditingService)
        {
            _personEditingService = personEditingService;
        }

        [HttpPut]

        public ActionResult<PersonDto> Edit([FromBody] PersonDto person)
        {
            var result = _personEditingService.Update(person);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]

        public ActionResult<PersonDto> GetUserInfo(int id)
        {
            var result = _personEditingService.Get(id);
            return CreateResponse(result);
        }
    }
}
