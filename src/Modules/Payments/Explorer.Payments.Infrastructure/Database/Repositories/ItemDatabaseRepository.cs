using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class ItemDatabaseRepository : CrudDatabaseRepository<Item, PaymentsContext>, IItemRepository
{
    public ItemDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }

    public Item GetByItemIdAndType(long itemId, ItemType type)
    {
        var item = DbContext.Items.FirstOrDefault(i => i.ItemId == itemId && i.Type == type);
        if (item == null) throw new KeyNotFoundException("Not found: " + itemId);
        return item;
    }

    public void DeleteByItemIdAndType(long itemId, ItemType type)
    {
        var item = DbContext.Items.FirstOrDefault(i => i.ItemId == itemId && i.Type == type);
        if (item == null) throw new KeyNotFoundException("Not found: " + itemId);
        DbContext.Items.Remove(item);
        DbContext.SaveChanges();
    }

}