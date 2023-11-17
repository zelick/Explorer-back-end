using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class SocialProfileService : ISocialProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISocialProfileRepository _socialProfileRepository;
        private readonly IMapper _mapper;

        public SocialProfileService(IUserRepository userRepository, ISocialProfileRepository socialProfileRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _socialProfileRepository = socialProfileRepository;
            _mapper = mapper;
        }

        public Result<SocialProfileDto> Follow(int followerId, int followedId)
        {
            if (followerId == followedId)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("You cannot follow yourself.");
            }

            var socialProfile = _socialProfileRepository.Get(followerId);
            var followedUser = _userRepository.GetUserById(followedId);

            socialProfile.Follow(followedUser);

            var result = _socialProfileRepository.Update(socialProfile);

            var socialProfileDto = _mapper.Map<SocialProfileDto>(result);

            return socialProfileDto;
        }

        public Result<SocialProfileDto> UnFollow(int followerId, int unFollowedUserId)
        {
            if (followerId == unFollowedUserId)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("You cannot unfollow yourself.");
            }
            var socialProfile = _socialProfileRepository.Get(followerId);
            var unFollowedUser = _userRepository.GetUserById(unFollowedUserId);

            socialProfile.UnFollow(unFollowedUser);

            var result = _socialProfileRepository.Update(socialProfile);

            var socialProfileDto = _mapper.Map<SocialProfileDto>(result);

            return socialProfileDto;
        }

        public Result<SocialProfileDto> Get(int userId)
        {
            var socialProfile = _socialProfileRepository.Get(userId);
            var socialProfileDto = _mapper.Map<SocialProfileDto>(socialProfile);

            return socialProfileDto;
        }
    }
}
