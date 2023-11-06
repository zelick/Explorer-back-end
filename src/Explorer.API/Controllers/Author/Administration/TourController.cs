using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    // [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourDto tour)
        {
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update([FromBody] TourDto tour)
        {
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("by-author/{authorId:int}")]
        public ActionResult<List<TourDto>> GetToursByAuthor([FromQuery] int page, [FromQuery] int pageSize, int authorId)
        {
            var result = _tourService.GetToursByAuthor(page, pageSize, authorId);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("add/{tourId:int}/{equipmentId:int}")]
        public ActionResult<TourDto> AddEquipment(int tourId, int equipmentId)
        {
            var result = _tourService.AddEquipment(tourId, equipmentId);
            return CreateResponse(result);
        }

        [HttpPut("remove/{tourId:int}/{equipmentId:int}")]
        public ActionResult<TourDto> RemoveEquipment(int tourId, int equipmentId)
        {
            var result = _tourService.RemoveEquipment(tourId, equipmentId);
            return CreateResponse(result);
        }

        [HttpGet("details/{id:int}")]
        public ActionResult<TourDto> Get(int id)
        {
            var result = _tourService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut("publishedTours/{id:int}")]
        public ActionResult<TourDto> Publish(int id)
        {
            var result = _tourService.Publish(id);
            return CreateResponse(result);
        }

        [HttpPut("archivedTours/{id:int}")]
        public ActionResult<TourDto> Archive(int id)
        {
            var result = _tourService.Archive(id);
            return CreateResponse(result);
        }

        [HttpPut("tourTime/{id:int}")]
        public ActionResult<TourDto> AddTime(TourTimesDto tourTimesDto, int id)
        {
            var result = _tourService.AddTime(tourTimesDto, id);
            return CreateResponse(result);
        }
    }
}
