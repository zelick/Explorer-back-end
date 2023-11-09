using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalCheckpointRequestService
    {
        Result<CheckpointRequestDto> Create(int checkpointId, int authorId, string status);
        Result<CheckpointRequestDto> AcceptRequest(int requestId);
        Result<CheckpointRequestDto> Get(int requestId);
    }
}
