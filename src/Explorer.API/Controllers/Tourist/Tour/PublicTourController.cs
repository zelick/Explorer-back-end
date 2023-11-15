using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/publicTours")]
    public class PublicTourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public PublicTourController(ITourService tourService)
        {
            _tourService = tourService;

        }

        [HttpGet("getAll")]
        public ActionResult<List<PublicTourDto>> GetPublicTours()
        {
            return CreateResponse(_tourService.GetPublicTours());
        }
    }
}
