using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class CheckpointService : CrudService<CheckpointDto, Checkpoint>, ICheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IInternalCheckpointRequestService _internalCheckpointRequestService;
        public CheckpointService(ICheckpointRepository repository, IMapper mapper, IInternalCheckpointRequestService internalCheckpointRequestService) : base(repository, mapper) 
        {
            _checkpointRepository = repository;
            _internalCheckpointRequestService = internalCheckpointRequestService;
        }

        public Result<PagedResult<CheckpointDto>> GetPagedByTour(int page, int pageSize, int id)
        {
            try
            {
                return MapToDto(_checkpointRepository.GetPagedByTour(page, pageSize, id));
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<CheckpointDto> Create(CheckpointDto checkpoint,int authorId, string status)
        {
            var result = Create(checkpoint);
            if (status.Equals("Public"))
            {
                _internalCheckpointRequestService.Create(Convert.ToInt32(result.Value.Id), authorId, "OnHold");
            }
            return result;
        }

        public Result DeleteChekcpointAndRequest(int id)
        {
            var request = _internalCheckpointRequestService.GetRequestByCheckpointId(id);
            if (request == null) throw new Exception($"Request for MapObject with ID {id} not found.");
            _internalCheckpointRequestService.Delete(request.Value.Id);
            return Delete(id);
        }
    }
}
