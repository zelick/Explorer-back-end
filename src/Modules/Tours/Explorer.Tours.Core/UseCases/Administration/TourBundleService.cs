using AutoMapper;
using AutoMapper.Execution;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourBundleService : CrudService<TourBundleDto, TourBundle>, ITourBundleService
	{
		private readonly ITourBundleRepository _tourBundleRepository;
        private readonly IInternalItemService _bundleItemService;
		private readonly ITourTourBundleRepository _tourTourBundleRepository;
		private readonly ITourRepository _tourRepository;


        public TourBundleService(ITourBundleRepository repository, ITourRepository tourRepo, IInternalItemService bundleItemService, ITourTourBundleRepository tourTourBundleRepository, IMapper mapper) : base(repository, mapper)
		{
			_tourBundleRepository = repository;
            _bundleItemService = bundleItemService;
			_tourTourBundleRepository = tourTourBundleRepository;
			_tourRepository = tourRepo;
		}

        public Result<PagedResult<TourBundleDto>> GetAllPublished(int page, int pageSize)
        {
            try
            { 
                var tourBundles = _tourBundleRepository.GetAllPublished(page, pageSize);
                List<long> ture = new List<long>();
                foreach (var b in tourBundles.Results)
                {
					foreach(var t in b.Tours)
					{
						ture.Add(t.Id);
                    }
                    b.Tours = _tourRepository.GetTours(ture);
                }
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
			List<Tour> bundleTours = new List<Tour>(tb.Tours);
			tb.Tours.Clear();
			try
			{
				var result = _tourBundleRepository.Create(tb);
				AddToursToBundle(bundleTours, result.Id);
				var bundleItemDto = new ItemDto()
                {
                    SellerId = result.AuthorId,
                    ItemId = result.Id,
                    Name = result.Name,
                    Price = result.Price,
                    Type = "Bundle",
                    BundleItemIds = bundleTours.Select(t => t.Id).ToList()
                };
                _bundleItemService.Create(bundleItemDto);
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
				var result = _tourBundleRepository.Update(tourBundle);
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

		public Result<List<TourBundleDto>> GetAllByAuthor(int page, int pageSize, int id)
		{
			try
			{
				var result = _tourBundleRepository.GetAllByAuthor(id);
				return MapToDto(result);
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}

		}


		public Result<TourBundleDto> GetBundleById(long bundleId)
		{
			try
			{
				var result = _tourBundleRepository.GetBundleWithTours(bundleId);
				return MapToDto(result);
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}

		public Result<TourBundleDto> RemoveTourFromBundle(int bundleId, int tourId)
		{
			try
			{
				var result = _tourTourBundleRepository.RemoveTourFromBundle(bundleId, tourId);
				var updatedBundle = GetBundleById((int)result.TourBundleId);
				return updatedBundle;
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}

		public Result<TourBundleDto> AddTourToBundle(int bundleId, int tourId)
		{
			try
			{
				var result = _tourTourBundleRepository.AddTourToBundle(bundleId, tourId);
				var updatedBundle = GetBundleById((int)result.TourBundleId);
				return updatedBundle;
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}
	}
}
