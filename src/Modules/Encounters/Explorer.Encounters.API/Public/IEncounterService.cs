using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterDto> Create(EncounterDto encounter);
        Result<EncounterDto> Update(EncounterDto tour);
        Result Delete(int id);
    }

}
