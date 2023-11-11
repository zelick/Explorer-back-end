using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicCheckpointService : CrudService<PublicCheckpointDto,  PublicCheckpoint>, IPublicCheckpointService
    {
        private readonly IInternalCheckpointRequestService _internalCheckpointRequestService;
        private readonly ICheckpointService _checkpointService;
        public PublicCheckpointService(ICrudRepository<PublicCheckpoint> repository, IMapper mapper, IInternalCheckpointRequestService internalCheckpointRequestService, ICheckpointService checkpointService) : base(repository, mapper)
        {
            _internalCheckpointRequestService = internalCheckpointRequestService;
            _checkpointService = checkpointService;
        }

        public Result<PublicCheckpointDto> Create(int checkpointRequestId, string notificationComment)
        {
            _internalCheckpointRequestService.AcceptRequest(checkpointRequestId, notificationComment);
            var request = _internalCheckpointRequestService.Get(checkpointRequestId);
            var checkpoint = _checkpointService.Get(Convert.ToInt32(request.Value.CheckpointId));
            PublicCheckpointDto publicCheckpoint = new PublicCheckpointDto();
            publicCheckpoint.TourId = checkpoint.Value.TourId;
            publicCheckpoint.Longitude = checkpoint.Value.Longitude;
            publicCheckpoint.Latitude = checkpoint.Value.Latitude;
            publicCheckpoint.Name = checkpoint.Value.Name;
            publicCheckpoint.Description = checkpoint.Value.Description;
            publicCheckpoint.Pictures = checkpoint.Value.Pictures;
            Create(publicCheckpoint);
            return publicCheckpoint;
        }
    }
}
