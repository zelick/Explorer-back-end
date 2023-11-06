using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain;
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
        private readonly IInternalObjectRequestService _internalObjectRequestService;
        public MapObjectService(ICrudRepository<MapObject> repository, IMapper mapper, IMapObjectRepository mapObjectRepository, IInternalObjectRequestService internalObjectRequestService) : base(repository, mapper)
        {
            _mapObjectRepository = mapObjectRepository;
            _internalObjectRequestService = internalObjectRequestService;
        }

        public Result<MapObjectDto> SetPublicStatus(int id)
        {
            var mapObject = _mapObjectRepository.SetPublicStatus(id);
            return MapToDto(mapObject);
        }

        public Result<MapObjectDto> Create(MapObjectDto mapObject, int userId, string status)
        {
            var result = Create(mapObject);
            if (status.Equals("public"))
            {
                ObjectRequestDto objectRequest = new ObjectRequestDto();
                objectRequest.AuthorId = userId;
                objectRequest.MapObjectId = result.Value.Id;
                objectRequest.Status = "OnHold";
                _internalObjectRequestService.Create(objectRequest);
            }
            return result;
        }
    }
}
