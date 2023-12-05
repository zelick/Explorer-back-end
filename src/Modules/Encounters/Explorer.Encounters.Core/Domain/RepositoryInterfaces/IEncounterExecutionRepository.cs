using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounters;
using System.Net;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterExecutionRepository : ICrudRepository<EncounterExecution>
    {
        List<EncounterExecution> GetAllByTourist(long touristId);
        List<EncounterExecution> GetAllCompletedByTourist(long touristId);
        EncounterExecution FindByEncounterId(long encounterId);
        EncounterExecution GetByEncounterAndTourist(long touristId, long encounterId);
        List<EncounterExecution> GetActiveByTourist(long touristId);
        List<EncounterExecution> UpdateRange(List<EncounterExecution> encounters);
        List<EncounterExecution> GetBySocialEncounter(long socialEncounterId);
    }
}
