using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourService
    {
        Result<List<TourDto>> GetToursByAuthor(int page, int pageSize, int id);
        Result<List<TourPreviewDto>> GetFilteredPublishedTours(int page, int pageSize);
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Create(TourDto tour);
        Result<TourDto> Update(TourDto tour);
        Result Delete(int id);
        Result<TourDto> AddEquipment(int tourId, int equipmentId);
        Result<TourDto> RemoveEquipment(int tourId, int equipmentId);
        Result<TourDto> Get(int id);
        public Result<TourPreviewDto> GetPublishedTour(int id);
        Result<TourDto> Publish(int id);
        Result<TourDto> AddTime(TourTimesDto tourTimesDto, int id);
        Result<TourDto> Archive(int id);
    }
}
