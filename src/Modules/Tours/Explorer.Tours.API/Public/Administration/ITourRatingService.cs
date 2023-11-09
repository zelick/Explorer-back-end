using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourRatingService
{
    Result<PagedResult<TourRatingDto>> GetPaged(int page, int pageSize);
    Result<TourRatingDto> Create(TourRatingDto tourRating);
    Result<TourRatingDto> Update(TourRatingDto tourRating);
    Result Delete(int id);
}
