using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private TourExecutionMapper _tourExecutionMapper;
        private readonly ITourRepository _tourRepository;
        public TourExecutionService(ITourExecutionRepository repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper) 
        {
            _tourExecutionRepository = repository;
            _tourRepository = tourRepository;
            _tourExecutionMapper = new TourExecutionMapper();
        }

        public Result<TourExecutionDto> CheckPosition(TouristPositionDto position, long id)
        {
            TourExecution tourExecution = CrudRepository.Get(id);
            TourExecution result=CrudRepository.Update(tourExecution.RegisterActivity(position.Longitude,position.Latitude));
            return MapToDto(result);

        }
        public Result<TourExecutionDto> Create(long touristId, long tourId)
        {
            try
            {
                var result = _tourExecutionRepository.Create(new TourExecution(touristId, tourId));
                result.setTour(_tourRepository.Get(tourId));
                return _tourExecutionMapper.createDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourExecutionDto> GetInProgressByTourAndTourist(long tourId, long touristId)
        {
            var result = _tourExecutionRepository.GetInProgressByTourAndTourist(tourId, touristId);
            if(result != null)
                return _tourExecutionMapper.createDto(result);
            return new Result<TourExecutionDto>();
        }
    }
}
