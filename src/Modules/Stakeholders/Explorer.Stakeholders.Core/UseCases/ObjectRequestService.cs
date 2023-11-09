using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ObjectRequestService : CrudService<ObjectRequestDto,ObjectRequest>, IObjectRequestService, IInternalObjectRequestService
    {
        private readonly IObjectRequestRepository _objectRequestRepository;
        private ObjectRequestMapper _objectRequestMapper;
        public ObjectRequestService(ICrudRepository<ObjectRequest> repository, IMapper mapper, IObjectRequestRepository objectRequestRepository) : base(repository, mapper)
        {
            _objectRequestRepository = objectRequestRepository;
            _objectRequestMapper = new ObjectRequestMapper();
        }

        public Result<ObjectRequestDto> AcceptRequest(int id)
        {
            var objectRequest = _objectRequestRepository.AcceptRequest(id);
            return MapToDto(objectRequest);
        }

        public Result<ObjectRequestDto> Create(int mapObjectId, int authorId, string status)
        {
            var objectRequest = _objectRequestMapper.createDto(mapObjectId, authorId, status);
            return Create(objectRequest);
        }

        public Result<List<ObjectRequestDto>> GetAll()
        {
            var objectRequests = CrudRepository.GetPaged(0, 0).Results.ToList();
            return MapToDto(objectRequests);
        }

        public Result<ObjectRequestDto> RejectRequest(int id)
        {
            var objectRequest = _objectRequestRepository.RejectRequest(id);
            return MapToDto(objectRequest);
        }
    }
}
