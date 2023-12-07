using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourBundleService
{
    Result<PagedResult<TourBundleDto>> GetAllPublished(int page, int pageSize);
    Result<PagedResult<TourBundleDto>> GetPaged(int page, int pageSize);
    Result<TourBundleDto> Create(TourBundleDto tourBundle);
    Result<TourBundleDto> Update(TourBundleDto tourBundle);
    Result Delete(int id);
	Result<List<TourBundleDto>> GetAllByAuthor(int page, int pageSize, int id);
	Result<TourBundleDto> GetBundleById(long bundleId);
	Result<TourBundleDto> RemoveTourFromBundle(int bundleId, int tourId);
	Result<TourBundleDto> AddTourToBundle(int bundleId, int tourId);

}