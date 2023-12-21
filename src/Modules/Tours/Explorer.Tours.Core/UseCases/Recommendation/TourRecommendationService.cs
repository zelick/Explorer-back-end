using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Recommendation;
using System;
using System.ComponentModel;

namespace Explorer.Tours.Core.UseCases.Recommendation
{
    public class TourRecommendationService : ITourRecommendationService
    {
        private readonly ITourService _tourService;
        private readonly ITourExecutionService _tourExecutionService;
        private readonly ITouristPositionService _touristPositionService;
        public TourRecommendationService(ITourExecutionService tourExecutionService,ITourService tourService,ITouristPositionService touristPositionService)
        {
           _tourExecutionService = tourExecutionService;
            _tourService = tourService;
            _touristPositionService = touristPositionService;
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
                        bool contains=false;
                        for (int j = 0; j < tours.Count; j++)
                        {
                            if (tours[j].Id == execution.Tour.Id)
                                contains=true;
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
            List<TourDto> tours= new List<TourDto>();
            foreach(TourDto tour in _tourService.GetTours().Value)
            {
                for (int i = 0; i < tours.Count; i++)
                {
                    if (IsCloseEnough(touristLongitude, touristLatitude, tour.Checkpoints[i].Longitude, tour.Checkpoints[i].Latitude))
                    {
                        bool contains = false;
                        for (int j = 0; j < tours.Count; j++)
                        {
                            if (tours[j].Id == tour.Id)
                                contains = true;
                        }
                        if (!contains)
                            tours.Add(tour);
                    }
                }                 
            }
            return tours;
        }

        public bool IsCloseEnough(double touristLongitude, double touristLatitude, double tourLongitude, double tourLatitude)
        {
            return GetTourDistanceFromTouristPosition(touristLongitude, touristLatitude, tourLongitude, tourLatitude) <= 40000;
        }

        public List<TourDto> GetAppropriateTours(int touristId)
        {
            TouristPositionDto touristPositionDto = _touristPositionService.GetPositionByCreator(0, 0, touristId).Value;
            List<TourDto> allTours = GetToursInRange(touristPositionDto.Longitude, touristPositionDto.Latitude);

            //PRVI ALGORITAM
            return null;
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
