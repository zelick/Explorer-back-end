using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingSetupService : IInternalShoppingSetupService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingSetupService(IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    public Result InitializeShopperFeatures(long userId)
    {
        try
        {
            _shoppingCartRepository.Create(new ShoppingCart(userId));
            // TODO create wallet
            return Result.Ok();
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}