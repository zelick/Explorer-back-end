using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tour
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/compositeTours")]
    public class CompositeTourController : BaseApiController
    {
        private readonly ICompositeTourService _tourService;


        public CompositeTourController(ICompositeTourService tourService)
        {
            _tourService = tourService;

        }

        [HttpGet]
        public ActionResult<PagedResult<CompositeTourDto>> GetAllComposite([FromQuery] int page, [FromQuery] int pageSize)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult<CompositeTourCreationDto> CreateComposite([FromBody] CompositeTourCreationDto tour)
        {
            return CreateResponse(_tourService.Create(tour));
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteComposite(int id)
        {
            throw new NotImplementedException();
        }
    }
}
