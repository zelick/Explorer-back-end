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
    public interface ICompositeTourService
    {
        Result<PagedResult<CompositeTourDto>> GetDetailedPaged(int page, int pageSize);
        Result<CompositeTourCreationDto> Create(CompositeTourCreationDto tour);
        Result Delete(int id);
        Result<CompositeTourDto> GetDetailed(int id);
    }
}
