using System.ComponentModel;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ItemService : BaseService<ItemDto, Item>, IInternalItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository, IMapper mapper) : base(mapper)
    {
        _itemRepository = itemRepository;
    }

    public Result Create(ItemDto itemDto)
    {
        try
        {
            _itemRepository.Create(MapToDomain(itemDto));

            return Result.Ok();
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result Update(ItemDto itemDto)
    {
        try
        {
            var item = MapToDomain(itemDto);
            var updatedItem = _itemRepository.GetByItemIdAndType(item.ItemId, item.Type);

            updatedItem.Update(item.Name, item.Price);
            _itemRepository.Update(updatedItem);

            return Result.Ok();
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result Delete(long id, string type)
    {
        try
        {
            var itemType = Enum.Parse<ItemType>(type);
            _itemRepository.DeleteByItemIdAndType(id, itemType);

            return Result.Ok();
        }
        catch (ArgumentException)
        {
            var validEnumValues = string.Join(", ", Enum.GetNames(typeof(ItemType)));
            var errorMessage = $"Invalid type. Valid enum values are: {validEnumValues}";

            return Result.Fail(FailureCode.InvalidArgument).WithError(errorMessage);
        }
    }
}