using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ICustomerRepository : ICrudRepository<Customer>
{
    public Customer GetByUser(long userId);
}

