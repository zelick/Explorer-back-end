using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Mappers;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicCheckpointService : CrudService<PublicCheckpointDto,  PublicCheckpoint>, IPublicCheckpointService
    {
        private readonly IInternalCheckpointRequestService _internalCheckpointRequestService;
        private readonly ICheckpointService _checkpointService;
        private PublicCheckpointMapper _publicCheckpointMapper;
        public PublicCheckpointService(ICrudRepository<PublicCheckpoint> repository, IMapper mapper, IInternalCheckpointRequestService internalCheckpointRequestService, ICheckpointService checkpointService) : base(repository, mapper)
        {
            _internalCheckpointRequestService = internalCheckpointRequestService;
            _checkpointService = checkpointService;
            _publicCheckpointMapper = new PublicCheckpointMapper();
        }

        public Result<PublicCheckpointDto> Create(int checkpointRequestId, string notificationComment)
        {
            _internalCheckpointRequestService.AcceptRequest(checkpointRequestId, notificationComment);
            var request = _internalCheckpointRequestService.Get(checkpointRequestId);
            var checkpoint = _checkpointService.Get(Convert.ToInt32(request.Value.CheckpointId));
            return Create(_publicCheckpointMapper.createDto(checkpoint.Value));
        }
    }
}
