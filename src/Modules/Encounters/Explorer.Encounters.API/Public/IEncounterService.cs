using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterDto> Create(EncounterDto encounter, long checkpointId, bool isSecretPrerequisite,long userId);
        Result<EncounterDto> CreateForTourist(EncounterDto encounter, long checkpointId, bool isSecretPrerequisite, long userId);
        Result<EncounterDto> Update(EncounterDto encounter, long userId);
        Result Delete(int id, int userId);
        Result<EncounterDto> Get(long id);
        List<EncounterExecutionDto> AddEncounters(List<EncounterExecutionDto> executions);
        EncounterExecutionDto AddEncounter(EncounterExecutionDto execution);
        Result<EncounterDto> GetRequestInfo(long encounterId);
    }

}
