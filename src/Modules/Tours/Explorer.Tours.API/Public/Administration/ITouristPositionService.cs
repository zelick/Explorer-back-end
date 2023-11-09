using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITouristPositionService
    {
        Result<TouristPositionDto> GetPositionByCreator(int page, int pageSize, int id);
        Result<PagedResult<TouristPositionDto>> GetPaged(int page, int pageSize);
        Result<TouristPositionDto> Create(TouristPositionDto position);
        Result<TouristPositionDto> Update(TouristPositionDto position);
        Result Delete(int id);
    }

}
