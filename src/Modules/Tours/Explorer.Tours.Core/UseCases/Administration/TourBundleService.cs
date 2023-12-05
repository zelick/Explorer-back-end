using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourBundleService : CrudService<TourBundleDto, TourBundle>, ITourBundleService
	{
		private readonly ITourBundleRepository _tourBundleRepository;
        private readonly IInternalItemService _bundleItemService;

        public TourBundleService(ITourBundleRepository repository, IInternalItemService bundleItemService, IMapper mapper) : base(repository, mapper)
		{
			_tourBundleRepository = repository;
            _bundleItemService = bundleItemService;
        }

        public Result<PagedResult<TourBundleDto>> GetAllPublished(int page, int pageSize)
        {
            try
            { 
                var tourBundles = _tourBundleRepository.GetAllPublished(page, pageSize);
                var tourBundleDtos = MapToDto(tourBundles);

                return tourBundleDtos;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourBundleDto> Create(TourBundleDto tourBundle)
		{
			TourBundle tb = MapToDomain(tourBundle);
			try
			{
				var result = CrudRepository.Create(tb);
                var bundleItemDto = new ItemDto()
                {
                    SellerId = result.AuthorId,
                    ItemId = result.Id,
                    Name = result.Name,
                    Price = result.Price,
                    Type = "Bundle",
                    BundleItemIds = result.Tours.Select(t => t.Id).ToList()
                };
                _bundleItemService.Create(bundleItemDto);
                return MapToDto(result);
			}
			catch (ArgumentException e)
			{
				return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
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
                    SellerId = result.AuthorId,
                    ItemId = result.Id,
                    Name = result.Name,
                    Price = result.Price,
                    Type = "Bundle",
                    BundleItemIds = result.Tours.Select(t => t.Id).ToList()
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
