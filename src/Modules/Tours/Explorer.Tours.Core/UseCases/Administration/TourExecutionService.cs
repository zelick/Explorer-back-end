using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Public.Recommendation;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases.Recommendation;


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private TourExecutionMapper _tourExecutionMapper;
        private readonly ITourRepository _tourRepository;
        private readonly IInternalTourOwnershipService _tourOwnershipService;
        private readonly IInternalFollowersService _touristFollowersService;
        private readonly TourPreviewMapper _tourPreviewMapper;
        public TourExecutionService(ITourExecutionRepository repository, IMapper mapper, ITourRepository tourRepository,
                                    IInternalTourOwnershipService tourOwnershipService, IInternalFollowersService touristFollowersService) : base(repository, mapper)
        {
            _tourExecutionRepository = repository;
            _tourRepository = tourRepository;
            _tourExecutionMapper = new TourExecutionMapper();
            _tourOwnershipService = tourOwnershipService;
            _touristFollowersService = touristFollowersService;
            _tourPreviewMapper = new TourPreviewMapper();
        }

        public Result<TourExecutionDto> CheckPosition(TouristPositionDto position, long id)
        {
            TourExecution tourExecution = CrudRepository.Get(id);
            TourExecution result = CrudRepository.Update(tourExecution.RegisterActivity(position.Longitude, position.Latitude));
            return _tourExecutionMapper.createDto(result);

        }
        public Result<TourExecutionDto> Create(long touristId, long tourId)
        {
            try
            {
                if(!_tourOwnershipService.IsTourPurchasedByUser(touristId, tourId).Value)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Tour not purchased");
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
            if (result != null)
                return _tourExecutionMapper.createDto(result);
            return new Result<TourExecutionDto>();
        }

        public Result<TourExecutionDto> Abandon(long id, long touristId)
        {
            try
            {
                if (!_tourOwnershipService.IsTourPurchasedByUser(touristId, id).Value)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Tour not purchased");

                TourExecution tourExecution = CrudRepository.Get(id);
                tourExecution.Abandone(id);
                return _tourExecutionMapper.createDto(CrudRepository.Update(tourExecution));
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            
        }

        public Result<List<TourPreviewDto>> GetSuggestedTours(long finishedTourId, long loggedUser, Result<List<TourPreviewDto>> foundedToursByAlgorithm)
        {
            var followerIds = _touristFollowersService.GetFollowerIds(loggedUser);

            var completedExecutions = _tourExecutionRepository.GetCompletedByTour(finishedTourId);

           //Ista kao i on,treba mi ID turiste
            var completedExecutionsByFollowers = completedExecutions.FindAll(f => followerIds.Contains(f.TouristId)).Distinct();

            var allCompletedExecutions = _tourExecutionRepository.GetAllCompleted();

            var suggestedTours = new List<TourPreview>();

            foreach (var ace in allCompletedExecutions)
            {
                foreach (var ce in completedExecutionsByFollowers)
                {
                    if (ace.TouristId == ce.TouristId && ace.TourId != finishedTourId)
                    {
                        suggestedTours.Add(ace.Tour.FilterView(ace.Tour));
                    }
                }
            }


            var result = new List<TourPreview>();

            foreach (var st in suggestedTours)
            {
                foreach (var ft in foundedToursByAlgorithm.Value)
                {
                    if (st.Id == ft.Id)
                    {
                        result.Add(st);
                    }
                }
            }

            return _tourPreviewMapper.createDtoList(result);

        }


        public Result<List<TourExecutionDto>> GetActiveTourExecutions()
        {
            try
            {
                var result = _tourExecutionRepository.GetActiveTourExecutions();
                List<TourExecutionDto> list = new List<TourExecutionDto>();
                foreach (var tourExecution in result) 
                {
                    list.Add(_tourExecutionMapper.createDto(tourExecution));
                }
                return list;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }


    }
}
