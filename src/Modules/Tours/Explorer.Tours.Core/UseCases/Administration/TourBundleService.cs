using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
	public class TourBundleService : CrudService<TourBundleDto, TourBundle>, ITourBundleService
	{
		private readonly ITourBundleRepository _tourBundleRepository;
		public TourBundleService(ITourBundleRepository repository, IMapper mapper) : base(repository, mapper)
		{
			_tourBundleRepository = repository;
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
				return Result.Ok();
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}
	}
}
