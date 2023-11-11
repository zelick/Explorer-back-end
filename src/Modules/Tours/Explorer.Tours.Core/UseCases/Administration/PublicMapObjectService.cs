using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicMapObjectService : CrudService<PublicMapObjectDto, PublicMapObject>, IPublicObjectService
    {
        private readonly IInternalObjectRequestService _internalObjectRequestService;
        private readonly IMapObjectService _mapObjectService;
        public PublicMapObjectService(ICrudRepository<PublicMapObject> repository, IMapper mapper, IInternalObjectRequestService internalObjectRequestService, IMapObjectService mapObjectService) : base(repository, mapper)
        {
            _internalObjectRequestService = internalObjectRequestService;
            _mapObjectService = mapObjectService;
        }

        public Result<PublicMapObjectDto> Create(int objectRequestId)
        {
            //_internalObjectRequestService.AcceptRequest(objectRequestId);
            var request = _internalObjectRequestService.Get(objectRequestId);
            var mapObject = _mapObjectService.Get(request.Value.MapObjectId);
            PublicMapObjectDto result = new PublicMapObjectDto();
            result.Name = mapObject.Value.Name;
            result.Description = mapObject.Value.Description;
            result.Longitude = mapObject.Value.Longitude;
            result.Latitude = mapObject.Value.Latitude;
            result.PictureURL = mapObject.Value.PictureURL;
            result.Category = mapObject.Value.Category;
            Create(result);
            return result;
        }
    }
}
