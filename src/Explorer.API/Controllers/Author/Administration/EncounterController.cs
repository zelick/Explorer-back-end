using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/encounter")]

    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

    }
}
