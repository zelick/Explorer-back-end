using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Stakeholders.API.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Explorer.Payments.Core.UseCases
{
    public class TouristWalletService : BaseService<TouristWalletDto, TouristWallet>, ITouristWalletService
    {
        private readonly ITouristWalletRepository _repository;
        private readonly IInternalNotificationService _internalNotificationService;

        public TouristWalletService(ITouristWalletRepository repository, IMapper mapper, IInternalNotificationService internalNotificationService) : base(mapper)
        {
            _repository = repository;
            _internalNotificationService = internalNotificationService;
        }

        public Result<TouristWalletDto> Create(TouristWalletDto dto)
        {
            var wallet = MapToDomain(dto);
            _repository.Create(wallet); 
            return dto;
        }

        public Result<TouristWalletDto> GetAdventureCoins(long userId)
        {
            var wallet = _repository.GetAdventureCoins(userId);
            return MapToDto(wallet);
        }

        public Result<TouristWalletDto> PaymentAdventureCoins(long userId, int coins)
        {
            var wallet = _repository.PaymentAdventureCoins(userId, coins);
            string description = "Uplaćeno Vam je " + coins + "AC.";
            _internalNotificationService.CreateWalletNotification(description, userId);
            return MapToDto(wallet);
        }
    }
}
