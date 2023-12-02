using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class CouponDatabaseRepository : CrudDatabaseRepository<Coupon, PaymentsContext>, ICouponRepository
    {
        public CouponDatabaseRepository(PaymentsContext dbContext) : base(dbContext) { }
    }
}
