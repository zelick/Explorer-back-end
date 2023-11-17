using Explorer.API.Services;
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
        private readonly ImageService _imageService;

        public ProfileEditingController(IPersonEditingService personEditingService)
        {
            _personEditingService = personEditingService;
            _imageService = new ImageService();
        }

        [HttpPut]
        public ActionResult<PersonDto> Edit([FromForm] PersonDto person, IFormFile profilePicture = null)
        {
            if (profilePicture != null)
            {
                var pictureUrl = _imageService.UploadImages(new List<IFormFile> { profilePicture });
                person.ProfilePictureUrl = pictureUrl[0];
            }
            
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
