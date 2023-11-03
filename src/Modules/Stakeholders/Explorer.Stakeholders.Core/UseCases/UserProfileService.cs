using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IUserFollowerRepository userFollowerRepository, IUserRepository userRepository, IMapper mapper)
        {
            _userFollowerRepository = userFollowerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void Follow(int userId, int followedUserId)
        {
            //_userFollowerRepository.AddFollower(userId, followedUserId);
            var user = _userRepository.GetUserById(userId);
            var followedUser = _userRepository.GetUserById(followedUserId);
            user.Followers.Add(followedUser);
            _userRepository.Update(user);
        }

        public Result<UserProfileDto> Get(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            var userProfileDto = _mapper.Map<UserProfileDto>(user);
            return userProfileDto;
        }
    }
}
