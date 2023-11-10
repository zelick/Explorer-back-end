using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourRatingService : CrudService<TourRatingDto, TourRating>, ITourRatingService
	{
		private readonly ITourRatingRepository _tourRatingRepository;
		public TourRatingService(ITourRatingRepository repository, IMapper mapper) : base(repository, mapper)
		{
			_tourRatingRepository = repository;
		}

		public Result<TourRatingDto> GetTourRating(int ratingId)
		{
			try
			{
				var result = _tourRatingRepository.GetTourRating(ratingId);
				return MapToDto(result);
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}
	}
}
