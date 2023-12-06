using Explorer.Encounters.API.Dtos;
using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Mappers
{
    public class EncounterRequestMapper
    {
        public EncounterRequestMapper() { }

        public EncounterRequestDto CreateDto(long userId, long encounterId, string status)
        {
           EncounterRequestDto dto = new EncounterRequestDto();

            dto.TouristId = userId;
            dto.EncounterId = encounterId;
            dto.Status = status;

            return dto;
        }
    }
}
