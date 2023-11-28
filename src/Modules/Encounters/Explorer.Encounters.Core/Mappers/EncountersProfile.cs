using AutoMapper;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain.Encounters;

namespace Explorer.Encounters.Core.Mappers
{
    public class EncountersProfile : Profile
    {
        public EncountersProfile() 
        {
            CreateMap<EncounterDto, Encounter>().ReverseMap();
            CreateMap<HiddenLocationEncounterDto, HiddenLocationEncounter>().ReverseMap();
            CreateMap<SocialEncounterDto, SocialEncounter>().ReverseMap();
        }
    }
}
