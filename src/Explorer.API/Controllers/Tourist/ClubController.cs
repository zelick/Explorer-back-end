using Explorer.API.Services;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;
        private readonly ImageService _imageService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
            _imageService = new ImageService();
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
         {
             var result = _clubService.GetPaged(page, pageSize);

             return CreateResponse(result);
         }

        [HttpGet("{id:int}")]
        public ActionResult<ClubDto> Get(int id)
        {
            var result = _clubService.GetClubWithUsers(id);
            return CreateResponse(result);
        }


        [HttpPost]
        public ActionResult<ClubDto> Create([FromForm] ClubDto club, [FromForm] List<IFormFile>? image = null)
        {
            if (image != null && image.Any())
            {
                var imageNames = _imageService.UploadImages(image);
                club.Image = imageNames[0];
            }

            var result = _clubService.Create(club);
            return CreateResponse(result);
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<ClubDto> Update([FromForm] ClubDto club, int id, [FromForm] List<IFormFile>? image = null)
        {
            if (image != null && image.Any())
            {
                var imageNames = _imageService.UploadImages(image);
                club.Image = imageNames[0];
            }

            club.Id = id;
            var result = _clubService.Update(club);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubService.Delete(id);
            return CreateResponse(result);
        }
    }
}