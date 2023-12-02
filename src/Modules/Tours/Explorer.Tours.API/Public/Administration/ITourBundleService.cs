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
	public interface ITourBundleService
	{
		//Result<PagedResult<TourBundleDto>> GetPaged(int page, int pageSize);
		Result<TourBundleDto> Create(TourBundleDto tourBundle);
		Result<TourBundleDto> Update(TourBundleDto tourBundle);
		Result Delete(int id);
	}
}
