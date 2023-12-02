using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Internal
{
    public interface IInternalCheckpointService
    {
        Result<CheckpointDto> SetEncounter(int id, long encounterId, bool isSecretPrerequisite, int userId);
        Result<CheckpointDto> Get(int id);
        Result<List<long>> GetEncountersByTour(int tourId);
    }
}
