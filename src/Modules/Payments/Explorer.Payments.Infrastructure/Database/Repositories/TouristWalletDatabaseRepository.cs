using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class TouristWalletDatabaseRepository : CrudDatabaseRepository<TouristWallet, PaymentsContext>, ITouristWalletRepository
    {
        private readonly PaymentsContext _dbContext;
        public TouristWalletDatabaseRepository(PaymentsContext dbContext) : base(dbContext) 
        { 
            _dbContext = dbContext;
        }

        public TouristWallet GetByUser(long userId)
        {
            var wallet = _dbContext.TouristWallets.FirstOrDefault(w => w.UserId == userId);
            if (wallet == null) throw new KeyNotFoundException("Not found wallet with user ID: " + userId);
            return wallet;
        }

        public TouristWallet PaymentAdventureCoins(long userId, int coins)
        {
            var wallet = _dbContext.TouristWallets.FirstOrDefault(w => w.UserId == userId);
            if (wallet == null) throw new KeyNotFoundException("Not found wallet with user ID: " + userId);

            try
            {
                wallet.PaymentAdventureCoins(coins);
                _dbContext.TouristWallets.Update(wallet);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return wallet;
        }
    }
}
