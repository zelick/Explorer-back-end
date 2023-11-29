using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/sale")]
    public class SaleController : BaseApiController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService) 
        {
            _saleService = saleService;
        }

        [HttpPost]
        public ActionResult<SaleDto> Create([FromBody] SaleDto saleDto)
        {
            var result = _saleService.Create(saleDto);
            return CreateResponse(result);
        }

        /*
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        */

        /*
        [HttpGet]
        public ActionResult<PagedResult<SaleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _saleService.GetPaged(page, pageSize);
            return CreateResponse(result);
            throw new NotImplementedException();
        }
        */
    }
}

       