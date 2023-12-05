using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Internal;

public interface IInternalItemService
{
    Result Create(ItemDto itemDto);
    Result Update(ItemDto itemDto);
    Result Delete(long id, string type);
}