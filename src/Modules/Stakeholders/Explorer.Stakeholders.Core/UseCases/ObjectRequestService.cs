using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ObjectRequestService : CrudService<ObjectRequestDto,ObjectRequest>, IObjectRequestService
    {
        private readonly IObjectRequestRepository _objectRequestRepository;
        public ObjectRequestService(ICrudRepository<ObjectRequest> repository, IMapper mapper, IObjectRequestRepository objectRequestRepository) : base(repository, mapper)
        {
            _objectRequestRepository = objectRequestRepository;
        }

        public Result<ObjectRequestDto> AcceptRequest(int id)
        {
            var objectRequest = _objectRequestRepository.AcceptRequest(id);
            return MapToDto(objectRequest);
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
