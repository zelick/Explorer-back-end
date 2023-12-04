using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourBundleService : CrudService<TourBundleDto, TourBundle>, ITourBundleService
	{
		private readonly ITourBundleRepository _tourBundleRepository;
        private readonly IInternalItemService _bundleItemService;
		private readonly ITourTourBundleRepository _tourTourBundleRepository;

        public TourBundleService(ITourBundleRepository repository, IInternalItemService bundleItemService, ITourTourBundleRepository tourTourBundleRepository, IMapper mapper) : base(repository, mapper)
		{
			_tourBundleRepository = repository;
            _bundleItemService = bundleItemService;
			_tourTourBundleRepository = tourTourBundleRepository;

		}

		public Result<TourBundleDto> Create(TourBundleDto tourBundle)
		{
			TourBundle tb = MapToDomain(tourBundle);
			List<Tour> bundleTours = new List<Tour>(tb.Tours);
			tb.Tours.Clear();
			try
			{
				var result = _tourBundleRepository.Create(tb);
				AddToursToBundle(bundleTours, result.Id);
				/*var bundleItemDto = new ItemDto()
                {
                    ItemId = result.Id,
                    Name = result.Name,
                    Price = result.Price,
                    Type = "Bundle"
                };
                //_bundleItemService.Create(bundleItemDto);*/
				return MapToDto(result);
			}
			catch (ArgumentException e)
			{
				return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
			}
		}

		public void AddToursToBundle(List<Tour> tours, long tbId)
		{
			foreach(Tour t in tours)
			{
				_tourTourBundleRepository.AddTourToBundle(tbId, t.Id);
			}
		}
		
		public Result<TourBundleDto> Update(TourBundleDto tourBundleDto)
		{
			var tourBundle = MapToDomain(tourBundleDto);
			try
			{
				var result = CrudRepository.Update(tourBundle);
                var bundleItemDto = new ItemDto()
                {
                    ItemId = result.Id,
                    Name = result.Name,
                    Price = result.Price,
                    Type = "Bundle"
                };
                _bundleItemService.Update(bundleItemDto);
                return MapToDto(result);
			}
			catch (ArgumentException e)
			{
				return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
			}
		}

		public Result Delete(int id)
		{
			try
			{
                CrudRepository.Delete(id);
                _bundleItemService.Delete(id, "Bundle");
                return Result.Ok();
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}
	}
}
