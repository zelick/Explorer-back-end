using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Shopping;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Tourism
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shopping-cart")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
           
        }

        [HttpGet]
        public ActionResult<PagedResult<ShoppingCartDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _shoppingCartService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ShoppingCartDto> Create([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _shoppingCartService.Create(shoppingCart);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ShoppingCartDto> Update([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _shoppingCartService.Update(shoppingCart);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _shoppingCartService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("checkShoppingCart/{touristId}")]
        public IActionResult CheckShoppingCart(int touristId)
        {
            var result = _shoppingCartService.CheckIfShoppingCartExists(touristId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("getShoppingCart/{touristId}")]
        public ActionResult<ShoppingCartDto> GetShoppingCart(int touristId)
        {
            var result = _shoppingCartService.GetShoppingCart(touristId);
            return CreateResponse(result);
        }

        //dodaj Item u Korpu i update korpa 
        [HttpPut("addItemToShoppingCart/{touristId}")]
        public ActionResult<ShoppingCartDto> AddItemToShoppingCart(OrderItemDto item, int touristId)
        {
            var result = _shoppingCartService.AddItemToShoppingCart(item, touristId);
            return CreateResponse(result);
        }

        [HttpDelete("deleteOrderItems/{shoppingCartId:int}")]
        public ActionResult DeleteOrderItems(long shoppingCartId)
        {
            var result = _shoppingCartService.DeleteOrderItems(shoppingCartId);
            return CreateResponse(result);
        }

    }
}
