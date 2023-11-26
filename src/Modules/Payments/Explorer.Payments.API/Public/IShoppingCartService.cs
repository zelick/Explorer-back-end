using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<ShoppingCartDto> GetByUser(long userId);
    Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCartDto, int userId);
    Result<ShoppingCartDto> CheckOut(long userId);
}