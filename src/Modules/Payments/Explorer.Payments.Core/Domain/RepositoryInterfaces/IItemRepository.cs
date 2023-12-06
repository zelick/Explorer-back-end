using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface IItemRepository : ICrudRepository<Item>
{
    public Item GetByItemIdAndType(long itemId, ItemType type);
    void DeleteByItemIdAndType(long itemId, ItemType type);
}