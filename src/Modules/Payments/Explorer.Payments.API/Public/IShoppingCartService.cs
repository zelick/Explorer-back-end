using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<ShoppingCartDto> GetByUser(long userId);
    Result<CouponDto> GetByCouponText(string couponText);
    Result<ShoppingCartDto> AddItem(ItemDto itemDto, int userId);
    Result<ShoppingCartDto> RemoveItem(ItemDto itemDto, int userId);
    Result<ShoppingCartDto> CheckOut(long userId, string? couponCode);
}