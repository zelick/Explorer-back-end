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

        public Result<List<long>> GetAuthorsStartedToursIds(long authorId)
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

        //za svaku turu dobijem koliko je ona zavrsena puta
        public Result<Dictionary<long, int>> GetTourCompletionNumberForEachTour(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetAuthorsFinishedToursIds(authorId).Value;
                Dictionary<long, int> tourCompletitionNumberForEachTour = new Dictionary<long, int>();

                foreach (long tourId in soldTourIds)
                {
                    if (tourCompletitionNumberForEachTour.ContainsKey(tourId))
                    {
                        tourCompletitionNumberForEachTour[tourId]++;
                    }
                    else
                    {
                        tourCompletitionNumberForEachTour[tourId] = 1;
                    }
                }

                return (Result<Dictionary<long, int>>)tourCompletitionNumberForEachTour;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //za svaku turu dobijem koliko je ona puta pokrenuta
        public Result<Dictionary<long, int>> GetTourStartedNumberForEachTour(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetAuthorsStartedToursIds(authorId).Value;
                Dictionary<long, int> touStartedNumberForEachTour = new Dictionary<long, int>();

                foreach (long tourId in soldTourIds)
                {
                    if (touStartedNumberForEachTour.ContainsKey(tourId))
                    {
                        touStartedNumberForEachTour[tourId]++;
                    }
                    else
                    {
                        touStartedNumberForEachTour[tourId] = 1;
                    }
                }

                return (Result<Dictionary<long, int>>)touStartedNumberForEachTour;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<Dictionary<long, double>> GetTourCompletionPercentageForEachTour(long authorId)
        {
            try
            {
                Result<Dictionary<long, int>> startedNumberResult = GetTourStartedNumberForEachTour(authorId);
                Result<Dictionary<long, int>> completionNumberResult = GetTourCompletionNumberForEachTour(authorId);

                Dictionary<long, int> startedNumberForEachTour = startedNumberResult.Value;
                Dictionary<long, int> completionNumberForEachTour = completionNumberResult.Value;

                Dictionary<long, double> completionPercentageForEachTour = new Dictionary<long, double>();

                foreach (long tourId in startedNumberForEachTour.Keys)
                {
                    int startedNumber = startedNumberForEachTour.ContainsKey(tourId) ? startedNumberForEachTour[tourId] : 0;
                    int completionNumber = completionNumberForEachTour.ContainsKey(tourId) ? completionNumberForEachTour[tourId] : 0;

                    double completionPercentage = (startedNumber == 0) ? 0 : ((double)completionNumber /(startedNumber + completionNumber)) * 100;

                    completionPercentageForEachTour.Add(tourId, completionPercentage);
                }

                return (Result<Dictionary<long, double>>)completionPercentageForEachTour;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<int> GetToursInCompletionRangeCount(long authorId, double minPercentage, double maxPercentage)
        {
            try
            {
                Result<Dictionary<long, double>> completionPercentageResult = GetTourCompletionPercentageForEachTour(authorId);
    
                Dictionary<long, double> completionPercentageForEachTour = completionPercentageResult.Value;

                int toursInRangeCount = completionPercentageForEachTour.Values
                    .Count(percentage => percentage >= minPercentage && percentage <= maxPercentage);

                return (Result<int>)toursInRangeCount;
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
            return GetAuthorsStartedToursIds(authorId).Value.Count();
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

        //da za svaki tourId iz autorovih tura
        //da nadjem koliko se puta on nasao u tour execution sa statusom inProgress - to je onda numOfStaredTours
        //da nadjem koliko se puta nasao u tour exection sa statusom Completed - to je onda numOfCompleted
        //ukupno - zbir ta dva
        //stavim za svaki tour id iracunam procenat toga i to stavim u listu
        //imacu listu double 50, 45, 25, 80 ... itd - to da budu procenti Completed
        //metoda koja izbroji koliko u toj listi imam ovih izmedju ovoga, ovih izmedju ovoga itd... i to da stavim isto u listu 
        //ta lista ima 4 elementa - na 0 (0-25), 1 (25-50) ITD

        //ALTERNATIVA
        //da iz fronta dobavljam za svaki opseg koliko imam broj tih 
        //posaljem iz fronta opseg 0, 24, pa opseg 25, 49 itd.. i onda brojim koliko tih upada

    }
}
