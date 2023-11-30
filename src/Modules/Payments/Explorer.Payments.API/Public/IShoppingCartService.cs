using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<ShoppingCartDto> GetByUser(long userId);
    Result<ShoppingCartDto> AddItem(OrderItemDto itemDto, int userId);
    Result<ShoppingCartDto> RemoveItem(OrderItemDto itemDto, int userId);
    Result<ShoppingCartDto> CheckOut(long userId);
}