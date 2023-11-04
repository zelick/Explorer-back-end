using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Shopping;
using Microsoft.EntityFrameworkCore;
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

        public Customer GetCustomerByTouristId(long touristId)
        {
            var customer = DbContext.Customers
                .Where(c => c.TouristId == touristId)
                .FirstOrDefault();

            return customer;
        }
    }
}
