using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping;

[Authorize(Policy = "touristPolicy")]
[Route("api/shopping/shopping-cart")]
public class ShoppingCartController : BaseApiController
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public ActionResult<ShoppingCartDto> GetByUser([FromQuery] int touristId)
    {
        if (User.PersonId() != touristId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

        var result = _shoppingCartService.GetByUser(touristId);
        return CreateResponse(result);
    }

    [HttpPut("add")]
    public ActionResult<ShoppingCartDto> AddItem([FromBody] ItemDto orderItem)
    {
        var result = _shoppingCartService.AddItem(orderItem, User.PersonId());
        return CreateResponse(result);
    }

    [HttpPut("remove")]
    public ActionResult<ShoppingCartDto> RemoveItem([FromBody] ItemDto orderItem)
    {
        var result = _shoppingCartService.RemoveItem(orderItem, User.PersonId());
        return CreateResponse(result);
    }

    [HttpPut("checkout")]
    public ActionResult<ShoppingCartDto> Checkout([FromQuery] int touristId)
    {
        if (User.PersonId() != touristId) return CreateResponse(Result.Fail(FailureCode.Forbidden));

        var result = _shoppingCartService.CheckOut(touristId);
        return CreateResponse(result);
    }
}