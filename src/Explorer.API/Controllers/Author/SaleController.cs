using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/sale")]
    public class SaleController : BaseApiController
    {
        private readonly ISaleService _saleService;
        private readonly ITourService _tourService;

        public SaleController(ISaleService saleService, ITourService tourService)
        {
            _saleService = saleService;
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<SaleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _saleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("active")]
        public ActionResult<List<SaleDto>> GetActiveSales()
        {
            var result = _saleService.GetActiveSales();
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<SaleDto> Create([FromBody] SaleDto saleDto)
        {
            if (saleDto.End < saleDto.Start)
            {
                return BadRequest("The sale end date cannot be before start date.");
            }
            if (saleDto.ToursIds.Count == 0)
            {
                return BadRequest("Please select at least one tour before creating a sale.");
            }
            if (!(saleDto.End - saleDto.Start > TimeSpan.FromDays(14)))
            {
                var result = _saleService.Create(saleDto);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("The sale end date cannot be set more than two weeks after the start date. Please choose a valid end date within this timeframe.");
            }
        }

        [HttpPut]
        public ActionResult<SaleDto> Update([FromBody] SaleDto saleDto)
        {
            if (User.PersonId() != saleDto.AuthorId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

            if (saleDto.End < saleDto.Start)
            {
                return BadRequest("The sale end date cannot be before start date.");
            }
            if (saleDto.ToursIds.Count == 0)
            {
                return BadRequest("Please select at least one tour before updating a sale.");
            }
            if (!(saleDto.End - saleDto.Start > TimeSpan.FromDays(14)))
            {
                var result = _saleService.Update(saleDto);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("The sale end date cannot be set more than two weeks after the start date. Please choose a valid end date within this timeframe.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _saleService.Delete(id, User.PersonId());
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SaleDto> Get(int id)
        {
            var result = _saleService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("tours-on-sale/{saleId:int}")]
        public ActionResult<List<TourDto>> GetToursFromSale(int saleId)
        {
            var sale = _saleService.Get(saleId);
            if (sale?.Value != null)
            {
                var tours = _tourService.GetToursFromSaleByIds(sale.Value.ToursIds);
                return CreateResponse(tours);
            }
            else
            {
                return NotFound("Sale not found");
            }
        }
    }
}

