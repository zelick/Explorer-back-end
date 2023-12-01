using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TouristWalletService : BaseService<TouristWalletDto, TouristWallet>, ITouristWalletService
    {
        private readonly ITouristWalletRepository _repository;

        public TouristWalletService(ITouristWalletRepository repository, IMapper mapper) : base(mapper)
        {
            _repository = repository;
        }

        public Result<TouristWalletDto> GetAdventureCoins(long userId)
        {
            var wallet = _repository.GetAdventureCoins(userId);
            return MapToDto(wallet);
        }

        public Result<TouristWalletDto> PaymentAdventureCoins(long userId, int coins)
        {
            var wallet = _repository.PaymentAdventureCoins(userId, coins);
            return MapToDto(wallet);
        }
    }
}
