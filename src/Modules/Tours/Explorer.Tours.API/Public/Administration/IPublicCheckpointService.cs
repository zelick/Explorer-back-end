using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IPublicCheckpointService
    {
        Result<PublicCheckpointDto> Create(PublicCheckpointDto publicCheckpoint);
        Result<PublicCheckpointDto> Update(PublicCheckpointDto publicCheckpoint);
        Result Delete(int id);
        Result<PublicCheckpointDto> Create(int checkpointRequestId);
        Result<PagedResult<PublicCheckpointDto>> GetPaged(int page, int pageSize);
    }
}
