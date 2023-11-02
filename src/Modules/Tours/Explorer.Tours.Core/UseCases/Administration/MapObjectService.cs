using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class MapObjectService : CrudService<MapObjectDto, MapObject>, IMapObjectService
    {
        private readonly IMapObjectRepository _mapObjectRepository;
        public MapObjectService(ICrudRepository<MapObject> repository, IMapper mapper, IMapObjectRepository mapObjectRepository) : base(repository, mapper)
        {
            _mapObjectRepository = mapObjectRepository;
        }

        public Result<MapObjectDto> SetPublicStatus(int id)
        {
            var mapObject = _mapObjectRepository.SetPublicStatus(id);
            return MapToDto(mapObject);
        }
    }
}
