using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterExecutionService
    {
        Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterExecution, long userId);
        Result<EncounterExecutionDto> Get(int id);
        Result<EncounterExecutionDto> Update(EncounterExecutionDto encounterExecution, long userId);
        Result Delete(int id, long userId);

        Result<PagedResult<EncounterExecutionDto>> GetAllByTourist(int touristId, int page, int pageSize);
        Result<PagedResult<EncounterExecutionDto>> GetAllCompletedByTourist(int touristId, int page, int pageSize);
        Result<EncounterExecutionDto> Activate(int touristId, double touristLatitude, double touristLongitude, int executionId);
        Result<EncounterExecutionDto> GetVisibleByTour(int tourId, double touristLongitude, double touristLatitude, int touristId);
        Result<int> CheckIfInRange(int id, double touristLongitude, double touristLatitude, int touristId);
        Result<List<EncounterExecutionDto>> GetActiveByTour(int touristId, int tourId);
        Result<EncounterExecutionDto> GetWithUpdatedLocation(int id, double touristLongitude, double touristLatitude, int touristId);
    }
}
