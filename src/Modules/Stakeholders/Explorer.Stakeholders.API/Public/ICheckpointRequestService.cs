using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface ICheckpointRequestService
    {
        Result<CheckpointRequestDto> Create(CheckpointRequestDto request);
        Result<CheckpointRequestDto> Update(CheckpointRequestDto request);
        Result<List<CheckpointRequestDto>> GetAll();
        Result<CheckpointRequestDto> AcceptRequest(int id, string notificationComment);
        Result<CheckpointRequestDto> RejectRequest(int id, string notificationComment);
    }
}
