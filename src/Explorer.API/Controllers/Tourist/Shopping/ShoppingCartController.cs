using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping
{
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
            var result = _shoppingCartService.GetByUser(touristId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ShoppingCartDto> Update(int id, [FromBody] ShoppingCartDto shoppingCart)
        {
            shoppingCart.Id = id;
            var result = _shoppingCartService.Update(shoppingCart);
            return CreateResponse(result);
        }

        [HttpPut("checkout")]
        public ActionResult<ShoppingCartDto> Checkout([FromQuery] int touristId)
        {
            var result = _shoppingCartService.CheckOut(touristId);
            return CreateResponse(result);
        }
    }
}