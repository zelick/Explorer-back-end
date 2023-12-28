using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.ShoppingSession;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingSetupService : IInternalShoppingSetupService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ITouristWalletRepository _touristWalletRepository;

    public ShoppingSetupService(IShoppingCartRepository shoppingCartRepository, ITouristWalletRepository touristWalletRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _touristWalletRepository = touristWalletRepository;
    }

    public Result InitializeShopperFeatures(long userId)
    {
        try
        {
            _shoppingCartRepository.Create(new ShoppingCart(userId));
            _touristWalletRepository.Create(new TouristWallet(userId));
            return Result.Ok();
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}