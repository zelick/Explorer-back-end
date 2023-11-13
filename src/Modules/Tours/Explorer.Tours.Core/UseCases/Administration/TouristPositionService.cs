using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TouristPositionService : CrudService<TouristPositionDto, TouristPosition>, ITouristPositionService
    {
        public TouristPositionService(ICrudRepository<TouristPosition> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TouristPositionDto> GetPositionByCreator(int page, int pageSize, int id)
        {
            var touristPosition = CrudRepository.GetPaged(page, pageSize);
            TouristPosition? positionByCreator = touristPosition.Results.FirstOrDefault(p => p.CreatorId == id);
            if (positionByCreator == null)
            {
                return new TouristPositionDto();
            }
            return MapToDto(positionByCreator);
        }

    }
}
