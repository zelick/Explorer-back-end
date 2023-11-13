using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ShoppingCartDatabaseRepository : CrudDatabaseRepository<ShoppingCart, StakeholdersContext>, IShoppingCartRepository
    {
        public ShoppingCartDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        { }

        public bool ShoppingCartExists(int touristId)
        {
            return DbContext.ShoppingCarts.Any(sc => sc.TouristId == touristId);
        }

        public ShoppingCart GetShoppingCart(int touristId)
        {
            // trebalo bi ovde da vrati celu order item listu, include?
            return DbContext.ShoppingCarts.FirstOrDefault(sc => sc.TouristId == touristId);
        }
    }
}
