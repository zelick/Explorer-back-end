using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class PaymentRecordDatabaseRepository : CrudDatabaseRepository<PaymentRecord, PaymentsContext>
{
    public PaymentRecordDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }
}