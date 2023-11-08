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
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IUserRepository userRepository, IMessageRepository messageRepository ,IMapper mapper)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public Result<UserProfileDto> Follow(int followerId, int followedId)
        {
            if(followerId == followedId) { throw new InvalidOperationException("You cannot follow yourself."); }
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

        public Result<MessageDto> SendMessage(MessageDto messageDto)
        {
            //var sender = _userRepository.GetUserById(messageDto.SenderId);
            //var recipient = _userRepository.GetUserById(messageDto.RecipientId);
            //var message = _mapper.Map<Message>(messageDto);
            //recipient.SendMessage(message);
            //_userRepository.Update(recipient);

            var sender = _userRepository.GetUserById(messageDto.SenderId);
            if (!sender.CanSendMessage(messageDto.RecipientId))
            {
                throw new InvalidOperationException("You can only send messages to your followers.");
            }

            var message = _mapper.Map<Message>(messageDto);
            var result = _messageRepository.Send(message);
            //var recipient = _userRepository.GetUserById(result.RecipientId);

            return _mapper.Map<MessageDto>(result);
        }

        public Result<MessageDto> MarkAsRead(int messageId)
        {
            var result = _messageRepository.MarkAsRead(messageId);
            return _mapper.Map<MessageDto>(result);
        }

        public Result<List<MessageDto>> GetNotifications(int userId)
        {
            var notifications = _messageRepository.GetAllUnread(userId);
            return _mapper.Map<List<MessageDto>>(notifications);
        }
    }
}
