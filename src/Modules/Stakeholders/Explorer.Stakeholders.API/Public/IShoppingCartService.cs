using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IShoppingCartService
    {
        Result<PagedResult<ShoppingCartDto>> GetPaged(int page, int pageSize);
        Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        Result Delete(int id);
        Result<bool> CheckIfShoppingCartExists(int touristId);
        Result<ShoppingCartDto> GetShoppingCart(int touristId);
        Result<ShoppingCartDto> AddItemToShoppingCart(OrderItemDto item, int touristId);
    }
}
