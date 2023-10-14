using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubRequestService : BaseService<ClubRequestDto, ClubRequest>, IClubRequestService
    {
        private readonly IClubRequestRepository _clubRequestRepository;
        private readonly IUserRepository _userRepository;

        public ClubRequestService(IClubRequestRepository clubRequestRepository, IUserRepository userRepository, IMapper mapper): base(mapper)
        {
            _clubRequestRepository = clubRequestRepository;
            _userRepository = userRepository;
        }

        public ClubRequestDto RequestToJoinClub(int touristId, int clubId)
        {
            //Provera da li postoji?
            // var tourist = _userRepository.GetUserById(touristId); 
            //var club = _clubRepository.GetClubById(clubId);

            var request = new ClubRequest(touristId, clubId, ClubRequestStatus.Processing);
            var savedRequest = _clubRequestRepository.Create(request);
            var resultDto = MapToDto(savedRequest);

            return resultDto;
        }
    }
}
