using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterRequestService : CrudService<EncounterRequestDto, EncounterRequest>, IEncounterRequestService
    {
        private readonly IEncounterRequestRepository _encounterRequestRepository;
        private readonly IEncounterRepository _encounterRepository;
        public EncounterRequestService(ICrudRepository<EncounterRequest> repository, IEncounterRequestRepository encounterRequestRepository, IMapper mapper, IEncounterRepository encounterRepository) : base(repository, mapper)
        {
            _encounterRequestRepository = encounterRequestRepository;
            _encounterRepository = encounterRepository;
        }

        public Result<EncounterRequestDto> AcceptRequest(int id)
        {
            var request = CrudRepository.Get(id);
            var requestAccepted = _encounterRequestRepository.AcceptRequest(id);
            var touristEncounter = _encounterRepository.Get(request.EncounterId);
            _encounterRepository.MakeEncounterPublished(touristEncounter.Id);
            return MapToDto(requestAccepted);
        }

        public Result<EncounterRequestDto> RejectRequest(int id)
        {
            var request = CrudRepository.Get(id);
            var reuestAccepted = _encounterRequestRepository.RejectRequest(id);
            return MapToDto(reuestAccepted);
        }

    }
}
