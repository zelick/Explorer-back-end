using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
	public class TourBundleService : CrudService<TourBundleDto, TourBundle>, ITourBundleService
	{
		private readonly ITourBundleRepository _tourBundleRepository;
		public TourBundleService(ITourBundleRepository repository, IMapper mapper) : base(repository, mapper)
		{
			_tourBundleRepository = repository;
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
		/*public Result<CheckpointDto> Update(CheckpointDto checkpoint, int userId)
		{
			Checkpoint c = MapToDomain(checkpoint);
			if (!c.IsAuthor(userId))
				return Result.Fail(FailureCode.InvalidArgument).WithError("Not checkpoint author");
			try
			{
				var result = CrudRepository.Update(c);
				return MapToDto(result);
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
			catch (ArgumentException e)
			{
				return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
			}
		}
		public Result Delete(int id, int userId)
		{
			Checkpoint c;
			try
			{
				c = _checkpointRepository.Get(id);
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
			if (!c.IsAuthor(userId))
				return Result.Fail(FailureCode.InvalidArgument).WithError("Not checkpoint author");
			try
			{
				CrudRepository.Delete(id);
				return Result.Ok();
			}
			catch (KeyNotFoundException e)
			{
				return Result.Fail(FailureCode.NotFound).WithError(e.Message);
			}
		}*/
	}
}
