using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Internal
{
    public interface IInternalCheckpointService
    {
        Result<CheckpointDto> UpdateEncounter(int id, long encounterId, bool isSecretPrerequisite);

    }
}
