using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class InternalCheckpointService : BaseService<CheckpointDto, Checkpoint>, IInternalCheckpointService
    {
        //private readonly ICheckpointService _checkpointService;
        private readonly ICheckpointRepository _checkpointRepository;

        public InternalCheckpointService(IMapper mapper/*, ICheckpointService checkpointService,*/,ICheckpointRepository checkpointRepository) : base(mapper)
        {
            //_checkpointService = checkpointService;
            _checkpointRepository = checkpointRepository;

        }

        public Result<CheckpointDto> Get(int id)
        {
            try
            {
                //var result = _checkpointService.Get(id).Value;

                //return result;
                return MapToDto(_checkpointRepository.Get(id));
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
            //CheckpointDto checkpoint;
            Checkpoint checkpoint;
            try
            {
                //checkpoint = _checkpointService.Get(id).Value;
                checkpoint=_checkpointRepository.Get(id);
                checkpoint.EncounterId = encounterId;
                checkpoint.IsSecretPrerequisite = isSecretPrerequisite;
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
                //return _checkpointService.Update(checkpoint, userId);
                return MapToDto(_checkpointRepository.Update(checkpoint));
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

        public Result<List<long>> GetEncountersByTour(int tourId)
        {
            List<long> encounters = new List<long>();
            try
            {
                var checkpoints = _checkpointRepository.GetPagedByTour(0, 0, tourId);
                foreach( var checkpoint in checkpoints.Results)
                {
                    if(checkpoint.EncounterId > 0 )
                        encounters.Add(checkpoint.EncounterId);
                }
                return encounters;
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

        public Result<long> GetEncounterId(int checkpointId)
        {
            try
            {
                return _checkpointRepository.Get(checkpointId).EncounterId.ToResult<long>();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

    }
}
