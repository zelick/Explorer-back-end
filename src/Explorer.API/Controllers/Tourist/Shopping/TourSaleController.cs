using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping;

[Authorize(Policy = "touristPolicy")]
[Route("api/shopping/sales")]
public class TourSaleController : BaseApiController
{
    private readonly ISaleService _saleService;

    public TourSaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public ActionResult<List<SaleDto>> GetActiveSales()
    {
        var result = _saleService.GetActiveSales();
        return CreateResponse(result);
    }
}