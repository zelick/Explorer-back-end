using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IInternalCheckpointService _internalCheckpointService;
        public EncounterService(IEncounterRepository encounterRepository,IInternalCheckpointService internalCheckpointService, IMapper mapper) : base(encounterRepository, mapper)
        {
            _encounterRepository= encounterRepository;
            _internalCheckpointService= internalCheckpointService;
        }

        public Result<EncounterDto> Create(EncounterDto encounterDto,long checkpointId,bool isSecretPrerequisite,long userId)
        {
            Encounter encounter = MapToDomain(encounterDto);
            Encounter result;
            if (!encounter.IsAuthor(userId)) 
                return Result.Fail(FailureCode.Forbidden); 

            try
            { 
                result = _encounterRepository.Create(new Encounter(encounter));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            try
            {
                Result<CheckpointDto> updateCheckpointResult= _internalCheckpointService.SetEncounter((int)checkpointId, result.Id, isSecretPrerequisite, (int)result.AuthorId);
                if (!updateCheckpointResult.IsSuccess && updateCheckpointResult.Reasons[0].Metadata.ContainsValue(404))
                    return Result.Fail(FailureCode.NotFound);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            return MapToDto(result);
        }

        public Result Delete(int checkpointId, int userId)
        {
            Result<CheckpointDto> updateCheckpointResult;
            Result<CheckpointDto> checkpoint;

            try
            {
                checkpoint = _internalCheckpointService.Get(checkpointId);
                updateCheckpointResult = _internalCheckpointService.SetEncounter(checkpointId,0, false, userId);
                if (!updateCheckpointResult.IsSuccess && updateCheckpointResult.Reasons[0].Metadata.ContainsValue(404))
                    return Result.Fail(FailureCode.NotFound);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            try
            {
                _encounterRepository.Delete(checkpoint.Value.EncounterId);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<EncounterDto> Update(EncounterDto encounterDto, long userId)
        {
            Encounter encounter = MapToDomain(encounterDto);
            if (!encounter.IsAuthor(userId))
                return Result.Fail(FailureCode.Forbidden).WithError("Not encounter author!");

            try
            {
                encounter.IsValid(encounter.Name, encounter.Description, encounter.AuthorId, encounter.XP, encounter.Longitude, encounter.Latitude, encounter.Status);
                var result = _encounterRepository.Update(encounter);
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
    }
}
