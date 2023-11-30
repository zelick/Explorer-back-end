using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class CustomerDatabaseRepository : CrudDatabaseRepository<Customer, PaymentsContext>, ICustomerRepository
{
    public CustomerDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }

    public Customer GetByUser(long userId)
    {
        var customer = DbContext.Customers.FirstOrDefault(c => c.UserId == userId);
        if (customer == null) throw new KeyNotFoundException("Not found: " + userId);
        return customer;
    }
}

