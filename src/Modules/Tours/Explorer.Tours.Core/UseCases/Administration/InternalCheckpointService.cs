using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class InternalCheckpointService : BaseService<CheckpointDto, Checkpoint>, IInternalCheckpointService
    {
        private readonly ICheckpointService _checkpointService;

        public InternalCheckpointService(IMapper mapper, ICheckpointService checkpointService) : base(mapper)
        {
            _checkpointService = checkpointService;
        }

        public Result<CheckpointDto> Get(int id)
        {
            try
            {
                var result = _checkpointService.Get(id).Value;
                return result;
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

        public Result<CheckpointDto> SetEncounter(int id, long encounterId, bool isSecretPrerequisite, int userId)
        {
            CheckpointDto checkpoint;
            try
            {
                checkpoint = _checkpointService.Get(id).Value;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            try
            {
                checkpoint.EncounterId = encounterId;
                checkpoint.IsSecretPrerequisite = isSecretPrerequisite;
                return _checkpointService.Update(checkpoint, userId);
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

    }
}
