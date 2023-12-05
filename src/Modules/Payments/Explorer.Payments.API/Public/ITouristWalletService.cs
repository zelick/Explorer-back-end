using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ITouristWalletService
    {
        public Result<TouristWalletDto> GetAdventureCoins(long userId);
        public Result<TouristWalletDto> PaymentAdventureCoins(long userId, int coins);
        public Result<TouristWalletDto> Create(TouristWalletDto dto);
    }
}
