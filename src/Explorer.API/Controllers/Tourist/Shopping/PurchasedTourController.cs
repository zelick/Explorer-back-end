using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping;

[Authorize(Policy = "touristPolicy")]
[Route("api/shopping/purchased-tours")]
public class PurchasedTourController : BaseApiController
{
    private readonly IItemOwnershipService _customerService;
    private readonly ITourService _tourService;

    public PurchasedTourController(IItemOwnershipService customerService, ITourService tourService)
    {
        _customerService = customerService;
        _tourService = tourService;
    }

    [HttpGet]
    public ActionResult<List<PurchasedTourPreviewDto>> GetUsersPurchasedTours([FromQuery] long touristId)
    {
        if (User.PersonId() != touristId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

        var result = _customerService.GetPurchasedToursByUser(touristId);
        if (result.IsFailed) return BadRequest("Purchased tours not found for the user.");
        var tours = _tourService.GetToursByIds(result.Value);
        return CreateResponse(tours);
    }

    [HttpGet("details/{purchasedTourId:int}")]
    public ActionResult<PurchasedTourPreviewDto> GetUsersPurchasedTourDetails(long purchasedTourId)
    {
        var result = _tourService.GetPurchasedTourById(purchasedTourId, User.PersonId());
        return CreateResponse(result);
    }
}