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
    public interface ITourExecutionService
    {
        Result<PagedResult<TourExecutionDto>> GetPaged(int page, int pageSize);
        Result<TourExecutionDto> Create(long touristId, long tourId);
        Result<TourExecutionDto> Update(TourExecutionDto tourExecution);
        Result Delete(int id);
        Result<TourExecutionDto> CheckPosition(TouristPositionDto position, long id);
    }
}
