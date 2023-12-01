using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class TouristWalletDatabaseRepository : CrudDatabaseRepository<TouristWallet, PaymentsContext>, ITouristWalletRepository
    {
        public TouristWalletDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }
    }
}
