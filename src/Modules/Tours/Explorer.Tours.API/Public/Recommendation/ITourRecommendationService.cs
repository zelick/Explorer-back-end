using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Recommendation
{
    public interface ITourRecommendationService
    {
        public double GetTourDistanceFromTouristPosition(double touristLongitude, double touristLatitude, double tourLongitude, double tourLatitude);

        public bool IsCloseEnough(double touristLongitude, double touristLatitude, double tourLongitude, double tourLatitude);

        public List<TourDto> GetToursInRange(double touristLongitude, double touristLatitude);
        public List<PurchasedTourPreviewDto> GetActiveToursInRange(double touristLongitude, double touristLatitude);
        Result<List<TourPreviewDto>> GetAppropriateTours(int touristId);

        public List<PurchasedTourPreviewDto> GetAppropriateActiveTours(int touristId);

    }
}
