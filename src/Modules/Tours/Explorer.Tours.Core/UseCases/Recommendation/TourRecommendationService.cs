using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Recommendation;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Recommendation
{
    public class TourRecommendationService : ITourRecommendationService
    {
        private readonly ITourService _tourService;
        private readonly ITourExecutionService _tourExecutionService;
        private readonly ITouristPositionService _touristPositionService;
        private readonly ITourPreferenceService _tourPreferenceService;
        public TourRecommendationService(ITourExecutionService tourExecutionService, ITourService tourService, ITouristPositionService touristPositionService, ITourPreferenceService tourPreferenceService)
        {
            _tourExecutionService = tourExecutionService;
            _tourService = tourService;
            _touristPositionService = touristPositionService;
            _tourPreferenceService = tourPreferenceService;
        }
        public List<PurchasedTourPreviewDto> GetActiveToursInRange(double touristLongitude, double touristLatitude)
        {
            List<PurchasedTourPreviewDto> tours = new List<PurchasedTourPreviewDto>();
            foreach (TourExecutionDto execution in _tourExecutionService.GetActiveTourExecutions().Value)
            {
                for (int i = 0; i < execution.Tour.Checkpoints.Count; i++)
                {
                    if (IsCloseEnough(touristLongitude, touristLatitude, execution.Tour.Checkpoints[i].Longitude, execution.Tour.Checkpoints[i].Latitude))
                    {
                        bool contains = false;
                        for (int j = 0; j < tours.Count; j++)
                        {
                            if (tours[j].Id == execution.Tour.Id)
                                contains = true;
                        }
                        if (!contains)
                            tours.Add(execution.Tour);
                    }
                }
            }
            return tours;
        }

        public double GetTourDistanceFromTouristPosition(double touristLongitude, double touristLatitude, double tourLongitude, double tourLatitude)
        {
            return Math.Acos(Math.Sin(Math.PI / 180 * (touristLatitude)) * Math.Sin(Math.PI / 180 * tourLatitude) + Math.Cos(Math.PI / 180 * touristLatitude) * Math.Cos(Math.PI / 180 * tourLatitude) * Math.Cos(Math.PI / 180 * touristLongitude - Math.PI / 180 * tourLongitude)) * 6371000;
        }

        public List<TourDto> GetToursInRange(double touristLongitude, double touristLatitude)
        {
            List<TourDto> tours = _tourService.GetTours().Value;
            List<TourDto> filteredTours = new List<TourDto>();
            foreach (TourDto tour in tours)
            {
                if (tour.Checkpoints != null)
                {
                    bool foundCloseEnough = false;
                    for (int i = 0; i < tour.Checkpoints.Count; i++)
                    {
                        if (!foundCloseEnough && IsCloseEnough(touristLongitude, touristLatitude, tour.Checkpoints[i].Longitude, tour.Checkpoints[i].Latitude))
                        {
                            filteredTours.Add(tour);
                            foundCloseEnough = true;
                            break;
                        }
                    }
                }
            }
            return filteredTours;
        }

        public bool IsCloseEnough(double touristLongitude, double touristLatitude, double tourLongitude, double tourLatitude)
        {
            return GetTourDistanceFromTouristPosition(touristLongitude, touristLatitude, tourLongitude, tourLatitude) <= 40000;
        }

        public Result<List<TourPreviewDto>> GetAppropriateTours(int touristId)
        {
            TouristPositionDto touristPositionDto = _touristPositionService.GetPositionByCreator(0, 0, touristId).Value;
            List<TourDto> allTours = GetToursInRange(touristPositionDto.Longitude, touristPositionDto.Latitude);
            List<int> algorithmPoints = Enumerable.Repeat(0, allTours.Count).ToList();
            TourPreferenceDto preference = _tourPreferenceService.GetPreferenceByCreator(0, 0, touristId).Value;
            int algorithmCounter = 0;
            foreach (TourDto tour in allTours)
            {
                TourPreviewDto dummyTour = _tourService.GetPublishedTour(tour.Id).Value;
                if (preference != null)
                {
                    algorithmPoints[algorithmCounter] += (tour.Tags.Intersect(preference.Tags).Count() * 3);
                    if (tour.DemandignessLevel != null && tour.DemandignessLevel.Equals(preference.Difficulty)) { algorithmPoints[algorithmCounter] += 2; }

                    if (dummyTour != null)
                    {
                        foreach (TourTimeDto tourTime in dummyTour.TourTime)
                        {
                            if (tourTime.Transportation.Contains("driving") && preference.Car == 1)
                            {
                                algorithmPoints[algorithmCounter] += 1;
                            }
                            if (tourTime.Transportation.Contains("cycling") && preference.Bike == 1)
                            {
                                algorithmPoints[algorithmCounter] += 1;
                            }
                        }
                    }
                }
                if (tour.TourRatings != null)
                {
                    double sumOfRatings = tour.TourRatings.Sum(obj => obj.Rating);
                    double averageRating = sumOfRatings / tour.TourRatings.Count();
                    if (averageRating > 4 && tour.TourRatings.Count() > 50)
                    {
                        algorithmPoints[algorithmCounter] += 50;
                    }

                    if (preference != null)
                    {
                        TourPreviewDto completedTour = _tourService.GetPublishedTour(tour.TourRatings[0].TourId).Value;
                        if (completedTour != null)
                        {
                            algorithmPoints[algorithmCounter] += (completedTour.Tags.Intersect(preference.Tags).Count() * 4);
                        }
                    }
                }
                algorithmCounter++;
            }

            var combinedList = allTours.Zip(algorithmPoints, (obj, intValue) => new { Object = obj, IntValue = intValue });
            var sortedList = combinedList.OrderBy(item => item.IntValue);
            List<TourDto> sortedObjects = sortedList.Select(item => item.Object).ToList();
            List<TourPreviewDto> frontendObjects = new List<TourPreviewDto>();
            foreach (var obj in sortedObjects)
            {
                frontendObjects.Add(_tourService.GetPublishedTour(obj.Id).Value);
            }
            //PRVI ALGORITAM
            return frontendObjects;
        }

        public List<PurchasedTourPreviewDto> GetAppropriateActiveTours(int touristId)
        {
            TouristPositionDto touristPositionDto = _touristPositionService.GetPositionByCreator(0, 0, touristId).Value;
            List<PurchasedTourPreviewDto> allActiveTours = GetActiveToursInRange(touristPositionDto.Longitude, touristPositionDto.Latitude);
            //DRUGI ALGORITAM
            return null;

        }
    }
}
