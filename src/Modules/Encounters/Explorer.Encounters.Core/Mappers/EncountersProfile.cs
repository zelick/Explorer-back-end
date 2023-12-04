using AutoMapper;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Tours.Core.Domain;

namespace Explorer.Encounters.Core.Mappers
{
    public class EncountersProfile : Profile
    {
        public EncountersProfile() 
        {
            CreateMap<EncounterDto, Encounter>()
                .ReverseMap();
            CreateMap<EncounterDto, HiddenLocationEncounter>()
                .ReverseMap();
            CreateMap<EncounterDto, SocialEncounter>()
                .ReverseMap();
            CreateMap<CompletedEncounterDto, CompletedEncounter>().ReverseMap();
            CreateMap<EncounterRequestDto, EncounterRequest>().ReverseMap();
            CreateMap<EncounterExecutionDto, EncounterExecution>().ReverseMap();
        }
        private EncounterStatus EncounterStatusFromString(string status)
        {
            if (Enum.TryParse<EncounterStatus>(status, true, out var statusResult))
            {
                return statusResult;
            }

            return EncounterStatus.Draft;
        }
    }

}
