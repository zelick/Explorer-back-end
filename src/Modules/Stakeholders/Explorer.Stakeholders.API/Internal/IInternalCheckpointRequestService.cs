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
        Result<CheckpointRequestDto> Create(CheckpointRequestDto request);
        Result<CheckpointRequestDto> AcceptRequest(int requestId, string notificationComment);
        Result<CheckpointRequestDto> Get(int requestId);
    }
}
