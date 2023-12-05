using Explorer.API.Services;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/encounter")]
    [Authorize(Policy = "authorPolicy")]


    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ImageService _imageService;


        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
            _imageService = new ImageService();

        }


        [HttpPost]
        public ActionResult<EncounterDto> Create([FromForm] EncounterDto encounter,[FromQuery] long checkpointId, [FromQuery] bool isSecretPrerequisite, [FromForm] List<IFormFile>? imageF = null)
        {

            if (imageF != null && imageF.Any())
            {
                var imageNames = _imageService.UploadImages(imageF);
                if (encounter.Type =="Location")
                    encounter.Image = imageNames[0];
            }

            var result = _encounterService.Create(encounter, checkpointId, isSecretPrerequisite,User.PersonId());
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<EncounterDto> Update([FromForm] EncounterDto encounter, [FromForm] List<IFormFile>? imageF = null)
        {

            if (imageF != null && imageF.Any())
            {
                var imageNames = _imageService.UploadImages(imageF);
                if (encounter.Type == "Location")
                    encounter.Image = imageNames[0];
            }

            var result = _encounterService.Update(encounter,User.PersonId());
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EncounterDto> GetById(int id)
        {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }

        //obrisi ovo 
        [HttpPut("{encounterId:int}/{touristId:int}")]
        public ActionResult<EncounterDto> FinishEncounter(int encounterId, int touristId)
        {
            var result = _encounterService.FinishEncounter(encounterId, touristId);
            return CreateResponse(result);
        }
    }
}
