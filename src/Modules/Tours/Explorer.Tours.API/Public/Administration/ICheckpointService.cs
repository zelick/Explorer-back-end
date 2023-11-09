using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ICheckpointService
    {
        Result<PagedResult<CheckpointDto>> GetPaged(int page, int pageSize);
        Result<CheckpointDto> Create(CheckpointDto checkpoint);
        Result<CheckpointDto> Update(CheckpointDto checkpoint);
        Result Delete(int id);
        Result<PagedResult<CheckpointDto>> GetPagedByTour(int page, int pageSize, int id);
        Result<CheckpointDto> Create(CheckpointDto checkpoint, int userId, string status);
        Result<CheckpointDto> Get(int id);
        Result<CheckpointDto> CreateChechpointSecreat(CheckpointSecretDto secret, int id);
        Result<CheckpointDto> UpdateChechpointSecreat(CheckpointSecretDto secret, int id);
        Result<CheckpointDto> DeleteChechpointSecreat(int id);

    }
}
