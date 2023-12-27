using Explorer.Encounters.API.Dtos;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Internal
{
    public interface IInternalEncounterExecutionService
    {
        Result<List<EncounterExecutionDto>> GetByEncounter(long encounterId);
    }
}
