using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ItemOwnershipService : IItemOwnershipService, IInternalTourOwnershipService
{
    private readonly ITourPurchaseTokenRepository _purchaseTokenRepository;

    public ItemOwnershipService(ITourPurchaseTokenRepository purchaseTokenRepository)
    {
        _purchaseTokenRepository = purchaseTokenRepository;
    }

    public Result<List<long>> GetPurchasedToursByUser(long userId)
    {
        try
        {
            var purchaseTokens = _purchaseTokenRepository.GetByUser(userId);

            return purchaseTokens.Select(t => t.TourId).ToList();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<bool> IsTourPurchasedByUser(long userId, long tourId)
    {
        try
        {
             return _purchaseTokenRepository.ExistsByTourAndUser(tourId, userId);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
    
    public Result<List<long>> GetSoldToursIds()
    {
        try
        {
            var purchaseTokens = _purchaseTokenRepository.GetSoldToursIds();
            return purchaseTokens;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}