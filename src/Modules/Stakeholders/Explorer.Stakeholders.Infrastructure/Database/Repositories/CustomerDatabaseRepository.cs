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
    public class CustomerDatabaseRepository : CrudDatabaseRepository<Customer, StakeholdersContext>, ICustomerRepository
    {
        public CustomerDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {

        }
    }
}
