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
		Result<PagedResult<TourBundleDto>> GetPaged(int page, int pageSize);
		Result<TourBundleDto> Create(TourBundleDto tourBundle);
		Result<TourBundleDto> Update(TourBundleDto tourBundle);
		Result Delete(int id);
		Result<List<TourBundleDto>> GetAllByAuthor(int page, int pageSize, int id);
		Result<TourBundleDto> GetBundleById(long bundleId);
		Result<TourBundleDto> RemoveTourFromBundle(int bundleId, int tourId);
		Result<TourBundleDto> AddTourToBundle(int bundleId, int tourId);
	}
}
