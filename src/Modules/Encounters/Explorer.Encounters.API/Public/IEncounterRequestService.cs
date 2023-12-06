using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;


namespace Explorer.Encounters.API.Public
{
    public interface IEncounterRequestService
    {
        Result<EncounterRequestDto> Create(EncounterRequestDto encounterRequest);
        Result<PagedResult<EncounterRequestDto>> GetPaged(int page, int pageSize);
        Result<EncounterRequestDto> AcceptRequest(int id);
        Result<EncounterRequestDto> RejectRequest(int id);
    }
}
