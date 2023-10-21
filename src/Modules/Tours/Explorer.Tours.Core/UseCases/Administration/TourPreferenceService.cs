using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourPreferenceService : CrudService<TourPreferenceDto, TourPreference>, ITourPreferenceService
    {
        public TourPreferenceService(ICrudRepository<TourPreference> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TourPreferenceDto> GetPreferenceByCreator(int page, int pageSize, int id)
        {
            var tourPreference = CrudRepository.GetPaged(page, pageSize);
            TourPreference? preferenceByCreator = tourPreference.Results.FirstOrDefault(p => p.CreatorId == id);
            if (preferenceByCreator == null)
            {
                return new TourPreferenceDto();
            }
            return MapToDto(preferenceByCreator);
        }

    }
}
