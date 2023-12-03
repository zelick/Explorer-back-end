using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterDto> Create(EncounterDto encounter, long checkpointId, bool isSecretPrerequisite,long userId);
        Result<EncounterDto> Update(EncounterDto encounter, long userId);
        Result Delete(int id, int userId);
        Result<EncounterDto> Get(long id);

        Result<List<EncounterExecutionDto>> AddEncounters(List<EncounterExecutionDto> executions);
        Result<EncounterDto> Activate(int id, double touristLongitude, double touristLatitude, int touristId);
        Result<int> CheckIfInRange(int id, double touristLongitude, double touristLatitude, int touristId);
    }

}
