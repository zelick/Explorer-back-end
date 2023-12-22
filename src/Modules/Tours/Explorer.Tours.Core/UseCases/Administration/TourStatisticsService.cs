using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourStatisticsService: ITourStatisticsService
    {
        private readonly IInternalTourOwnershipService _internalTourOwnershipService;
        private readonly ITourRepository _tourRepository;
        private readonly ITourExecutionRepository _tourExecutionRepository;
        public TourStatisticsService(IInternalTourOwnershipService internalTourOwnershipService, ITourRepository tourRepository, ITourExecutionRepository tourExecutionRepository)
        {
            _internalTourOwnershipService = internalTourOwnershipService;
            _tourRepository = tourRepository;
            _tourExecutionRepository = tourExecutionRepository;
        }

        public Result<List<long>> GetSoldToursIds()
        {
            return _internalTourOwnershipService.GetSoldToursIds();
        }

        public Result<List<long>> GetStartedToursIds()
        {
            return _tourExecutionRepository.GetStartedToursIds();
        }

        public Result<List<long>> GetFinishedToursIds()
        {
            return _tourExecutionRepository.GetFinishedToursIds();
        }

        public Result<List<Tour>> GetPublishedToursByAuthor(long authorId)
        {
            return _tourRepository.GetPublishedToursByAuthor(authorId);
        }

        /*
        public Result<List<Tour>> GetPublishedAndSoldToursByAuthor(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetSoldToursIds().Value;

                List<Tour> publishedTours = GetPublishedToursByAuthor(authorId).Value;

                List<Tour> publishedAndSoldTours = publishedTours
                    .Where(t => soldTourIds.Contains(t.Id))
                    .ToList();

                return (Result<List<Tour>>)publishedAndSoldTours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        */

        //kasnije ovo prebaciti u celu listu tura ako bude trebalo - PREBACIS U CELE TURE
        public Result<List<long>> GetAuthorsPublishedSoldToursIds(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetSoldToursIds().Value;

                List<Tour> publishedTours = GetPublishedToursByAuthor(authorId).Value;

                List<long> soldTourIdsInPublishedTours = soldTourIds
                    .Where(id => publishedTours.Any(t => t.Id == id))
                    .ToList();

                return (Result<List<long>>)soldTourIdsInPublishedTours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<List<long>> GetAuthorsStartedSToursIds(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetStartedToursIds().Value;

                List<Tour> publishedTours = GetPublishedToursByAuthor(authorId).Value;

                List<long> soldTourIdsInPublishedTours = soldTourIds
                    .Where(id => publishedTours.Any(t => t.Id == id))
                    .ToList();

                return (Result<List<long>>)soldTourIdsInPublishedTours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<List<long>> GetAuthorsFinishedToursIds(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetFinishedToursIds().Value;

                List<Tour> publishedTours = GetPublishedToursByAuthor(authorId).Value;

                List<long> soldTourIdsInPublishedTours = soldTourIds
                    .Where(id => publishedTours.Any(t => t.Id == id))
                    .ToList();

                return (Result<List<long>>)soldTourIdsInPublishedTours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<int> GetAuthorsSoldToursNumber(long authorId)
        {
            return GetAuthorsPublishedSoldToursIds(authorId).Value.Count();
        }

        public Result<int> GetAuthorsStartedToursNumber(long authorId)
        {
            return GetAuthorsStartedSToursIds(authorId).Value.Count();
        }

        public Result<int> GetAuthorsFinishedToursNumber(long authorId)
        {
            return GetAuthorsFinishedToursIds(authorId).Value.Count();
        }

        public Result<double> GetAuthorsTourCompletionPercentage(long authorId)
        {
            try
            {
                int startedToursCount = GetAuthorsStartedToursNumber(authorId).Value;
                int finishedToursCount = GetAuthorsFinishedToursNumber(authorId).Value;

                if (startedToursCount == 0)
                {
                    return (Result<double>)0;
                }

                double completionPercentage = (double)finishedToursCount / (startedToursCount + finishedToursCount) * 100;
                return (Result<double>)completionPercentage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
