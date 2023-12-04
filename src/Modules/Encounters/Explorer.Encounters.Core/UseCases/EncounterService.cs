using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System.Diagnostics.Metrics;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IMapper _mapper;
        private readonly IInternalCheckpointService _internalCheckpointService;
        private readonly IEncounterExecutionRepository _encounterExecutionRepository;
        private readonly IInternalTouristService _internalTouristService;
        public EncounterService(IEncounterRepository encounterRepository,IInternalCheckpointService internalCheckpointService, IEncounterExecutionRepository encounterExecutionRepository, IInternalTouristService internalTouristService, IMapper mapper) : base(encounterRepository, mapper)
        {
            _encounterRepository = encounterRepository;
            _internalCheckpointService = internalCheckpointService;
            _mapper = mapper;
            _encounterExecutionRepository = encounterExecutionRepository;
            _internalTouristService = internalTouristService;
        }

        public Result<EncounterDto> Create(EncounterDto encounterDto,long checkpointId,bool isSecretPrerequisite,long userId)
        {
            Encounter result;
            Encounter encounter =new Encounter();
            if (encounterDto.Type == "Location")
                try 
                {
                    encounter = _mapper.Map<EncounterDto, HiddenLocationEncounter>(encounterDto);
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }
            else if (encounterDto.Type == "Social")
                try
                {
                    encounter = _mapper.Map<EncounterDto, SocialEncounter>(encounterDto);
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }
            else
                try
                {
                    encounter = _mapper.Map<EncounterDto, Encounter>(encounterDto);
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }


            if (!encounter.IsAuthor(userId)) 
                return Result.Fail(FailureCode.Forbidden); 
            
            try
            {
                encounter.IsValid(encounter.Name, encounter.Description, encounter.AuthorId, encounter.XP, encounter.Longitude, encounter.Latitude, encounter.Status);
                result = _encounterRepository.Create(encounter);
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

        public Result<EncounterDto> Get(long id)
        {
            return MapToDto(_encounterRepository.Get(id));
  
        }

        public Result<EncounterDto> Update(EncounterDto encounterDto, long userId)
        {
            Encounter encounter = new Encounter();
            if (encounterDto.Type == "Location")
                try
                {
                    encounter = _mapper.Map<EncounterDto, HiddenLocationEncounter>(encounterDto);
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }
            else if (encounterDto.Type == "Social")
                try
                {
                    encounter = _mapper.Map<EncounterDto, SocialEncounter>(encounterDto);
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }
            else
                try
                {
                    encounter = _mapper.Map<EncounterDto, Encounter>(encounterDto);
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }

            if (!encounter.IsAuthor(userId))
                return Result.Fail(FailureCode.Forbidden).WithError("Not encounter author!");

            try
            {
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

        public Result<EncounterDto> FinishEncounter(long encounterId, long touristId)
        {
            //update Encounter
            Encounter encounter = new Encounter();
            encounter = _encounterRepository.Get(encounterId);
            encounter.FinishEncounter(touristId);
            var updatedEncounter = _encounterRepository.Update(encounter);
            //update EncounterExecution 
            var encounterExecution = _encounterExecutionRepository.FindByEncounterId(encounterId);
            if (encounterExecution != null)
            {
                encounterExecution.FinishEncounter();
                _encounterExecutionRepository.Update(encounterExecution);
            }
            //update Tourist
            var toruistDto = _internalTouristService.UpdateTouristXpAndLevel(touristId, encounter.XP);

            return MapToDto(updatedEncounter);
        }

    }
}
