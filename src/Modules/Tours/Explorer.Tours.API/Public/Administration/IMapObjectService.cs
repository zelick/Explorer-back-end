using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IMapObjectService
    {
        Result<PagedResult<MapObjectDto>> GetPaged(int page, int pageSize);
        Result<MapObjectDto> Create(MapObjectDto mapObject);
        Result<MapObjectDto> Update(MapObjectDto mapObject);
        Result Delete(int id);
        Result<MapObjectDto> Get(int id);
        Result<MapObjectDto> Create(MapObjectDto mapObject, int userId, string status);
        Result DeleteObjectAndRequest(int id);
    }
}
