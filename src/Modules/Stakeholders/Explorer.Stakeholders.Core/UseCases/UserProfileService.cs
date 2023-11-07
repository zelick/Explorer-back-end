using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Result<UserProfileDto> Follow(int followerId, int followedId)
        {
            var followedUser = _userRepository.GetUserById(followedId);
            var followerUser = _userRepository.GetUserById(followerId);
            followedUser.Follow(followerUser);
            _userRepository.Update(followedUser);

            return _mapper.Map<UserProfileDto>(followerUser);
        }

        public Result<UserProfileDto> Get(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            return _mapper.Map<UserProfileDto>(user);
        }

        public Result<UserProfileDto> SendMessage(MessageDto messageDto)
        {
            //var sender = _userRepository.GetUserById(messageDto.SenderId);
            var recipient = _userRepository.GetUserById(messageDto.RecipientId);
            var message = _mapper.Map<Message>(messageDto);
            recipient.SendMessage(message);
            _userRepository.Update(recipient);

            return _mapper.Map<UserProfileDto>(recipient);
        }
    }
}
