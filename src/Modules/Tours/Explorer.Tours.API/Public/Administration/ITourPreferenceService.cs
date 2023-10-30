using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourPreferenceService
    {

        Result<TourPreferenceDto> GetPreferenceByCreator(int page, int pageSize, int id);
        Result<PagedResult<TourPreferenceDto>> GetPaged(int page, int pageSize);
        Result<TourPreferenceDto> Create(TourPreferenceDto preference);
        Result<TourPreferenceDto> Update(TourPreferenceDto preference);
        Result Delete(int id);



    }
}
