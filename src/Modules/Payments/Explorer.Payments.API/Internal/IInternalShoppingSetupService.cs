using FluentResults;

namespace Explorer.Payments.API.Internal;

public interface IInternalShoppingSetupService
{
    Result InitializeShopperFeatures(long userId);
}

