using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalObjectRequestService
    {
        Result<ObjectRequestDto> Create(ObjectRequestDto request);
        Result<ObjectRequestDto> AcceptRequest(int objectRequestId, string notificationComment);
        Result<ObjectRequestDto> Get(int objectRequestId);
    }
}
