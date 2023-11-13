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
    public interface IPublicObjectService
    {
        Result<PublicMapObjectDto> Create(PublicMapObjectDto publicMapObject);
        Result<PublicMapObjectDto> Update(PublicMapObjectDto publicMapObject);
        Result Delete(int id);
        Result<PagedResult<PublicMapObjectDto>> GetPaged(int page, int pageSize);
        Result<PublicMapObjectDto> Create(int objectRequestId, string notificationComment);
    }
}
