using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
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

        public Result<CheckpointDto> Create(CheckpointDto checkpoint, int userId)
        {
            Checkpoint c = MapToDomain(checkpoint);
            if(!c.IsAuthor(userId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Not checkpoint author");
            try
            {
                var result = CrudRepository.Create(c);
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<CheckpointDto> Update(CheckpointDto checkpoint, int userId)
        {
            Checkpoint c = MapToDomain(checkpoint);
            if (!c.IsAuthor(userId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Not checkpoint author");
            try
            {
                var result = CrudRepository.Update(c);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result Delete(int id, int userId)
        {
            Checkpoint c;
            try
            {
            c = _checkpointRepository.Get(id);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            if (!c.IsAuthor(userId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Not checkpoint author");
            try
            {
                CrudRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
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
            if (status.Equals("public"))
            {
                _internalCheckpointRequestService.Create(Convert.ToInt32(checkpoint.Id), authorId, "OnHold");
            }
            return result;
        }

        public Result<CheckpointDto> CreateChechpointSecreat(CheckpointSecretDto secret,int id)
        {
            Checkpoint checkpoint = _checkpointRepository.Get(id);
            return MapToDto(_checkpointRepository.Update(checkpoint.CreateCheckpointSecret(secret.Description, secret.Pictures)));
        }

        public Result<CheckpointDto> UpdateChechpointSecreat(CheckpointSecretDto secret, int id)
        {
            Checkpoint checkpoint = _checkpointRepository.Get(id);
            return MapToDto(_checkpointRepository.Update(checkpoint.UpdateCheckpointSecret(secret.Description, secret.Pictures)));
        }
        public Result<CheckpointDto> DeleteChechpointSecreat(int id)
        {
            Checkpoint checkpoint = _checkpointRepository.Get(id);
            return MapToDto(_checkpointRepository.Update(checkpoint.DeleteCheckpointSecret()));
        }

        public Result<CheckpointDto> Get(int id)
        {
            return MapToDto(_checkpointRepository.Get(id));
        }
    }
}
