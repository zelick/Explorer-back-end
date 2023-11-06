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
    }
}
