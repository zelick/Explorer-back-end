using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/sale")]
    public class SaleController : BaseApiController
    {
        private readonly ISaleService _saleService;
        private readonly ITourService _tourService;

        public SaleController(ISaleService saleService) 
        {
            _saleService = saleService;
        }

        [HttpGet]
        public ActionResult<PagedResult<SaleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _saleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<SaleDto> Create([FromBody] SaleDto saleDto)
        {
            var result = _saleService.Create(saleDto);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<SaleDto> Update([FromBody] SaleDto saleDto)
        {
            var result = _saleService.Update(saleDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _saleService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SaleDto> Get(int id)
        {
            /*
            var result = _saleService.Get(id);
            return CreateResponse(result);
            */
            throw new NotImplementedException();
        }

        //ovo ce morati u neki touristPolicy
        [HttpGet("tours-on-sale/{saleId:int}")]
        public ActionResult<List<PublishedTourDto>> GetToursFromSale([FromQuery] int saleId)
        {
            var sale = _saleService.Get(saleId);
            var tours = _tourService.GetToursFromSaleByIds(sale.Value.ToursIds);
            return CreateResponse(tours);
        }



        /*
        [HttpGet("tours-on-sale/{saleId:int}")]
        public ActionResult<List<PublishedTourDto>> GetToursFromSale([FromQuery] long saleId)
        {
            var result = _saleService.GetToursFromSale(saleId);

            var result = _customerService.GetPurchasedToursByUser(touristId);
            if (result.IsFailed) return BadRequest("Purchased tours not found for the user.");
            var tours = _tourService.GetToursByIds(result.Value);
            return CreateResponse(tours);
        }
        */
    }
}

       