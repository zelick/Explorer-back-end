using Explorer.API.Services;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Encounters
{
    [Route("api/administration/touristEncounter")]
    public class TouristEncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ImageService _imageService;


        public TouristEncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
            _imageService = new ImageService();

        }

        [HttpPost]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<EncounterDto> Create([FromForm] EncounterDto encounter, [FromQuery] long checkpointId, [FromQuery] bool isSecretPrerequisite, [FromForm] List<IFormFile>? imageF = null)
        {

            if (imageF != null && imageF.Any())
            {
                var imageNames = _imageService.UploadImages(imageF);
                if (encounter.Type == "Location")
                    encounter.Image = imageNames[0];
            }

            var result = _encounterService.CreateForTourist(encounter, checkpointId, isSecretPrerequisite, User.PersonId());
            return CreateResponse(result);
        }

        [HttpPut]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<EncounterDto> Update([FromForm] EncounterDto encounter, [FromForm] List<IFormFile>? imageF = null)
        {

            if (imageF != null && imageF.Any())
            {
                var imageNames = _imageService.UploadImages(imageF);
                if (encounter.Type == "Location")
                    encounter.Image = imageNames[0];
            }

            var result = _encounterService.Update(encounter, User.PersonId());
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult Delete(int id)
        {
            var result = _encounterService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<EncounterDto> GetById(int id)
        {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("requestInfo/{encounterId:long}")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<EncounterDto> GetRequestInfo(long encounterId)
        {
            var result = _encounterService.GetRequestInfo(encounterId);
            return CreateResponse(result);
        }
    }
}
