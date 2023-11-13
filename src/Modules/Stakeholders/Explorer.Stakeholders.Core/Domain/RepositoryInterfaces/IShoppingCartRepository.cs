using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository : ICrudRepository<ShoppingCart>
    {
        bool ShoppingCartExists(int touristId);
        ShoppingCart GetShoppingCart(int touristId);
    }
}
