using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class MessageService : CrudService<MessageDto, Message>, IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISocialProfileRepository _socialProfileRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository, ISocialProfileRepository socialProfileRepository, IMapper mapper) : base(messageRepository, mapper)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _socialProfileRepository = socialProfileRepository;
        }

        public Result<MessageDto> Send(MessageDto messageDto)
        {
            if (!CanSend(messageDto) || messageDto.SenderId == messageDto.RecipientId)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("You cannot send message.");
            }
            messageDto.SenderUsername = _userRepository.GetUserById(messageDto.SenderId).Username;
            var message = _messageRepository.Send(MapToDomain(messageDto));

            return MapToDto(message);
        }

        public Result<List<MessageDto>> GetAllSent(int userId)
        {
            var sentMessages = _messageRepository.GetAllSent(userId);
            return MapToDto(sentMessages);
        }

        public Result<List<MessageDto>> GetAllReceived(int userId)
        {
            var receivedMessages = _messageRepository.GetAllReceived(userId);
            return MapToDto(receivedMessages);
        }

        public Result<List<MessageDto>> GetAllUnread(int userId)
        {
            var unreadMessages = _messageRepository.GetAllUnread(userId);
            return MapToDto(unreadMessages);
        }

        private bool CanSend(MessageDto messageDto)
        {
            var isSenderExists = _userRepository.GetAll().Any(u => u.Id == messageDto.SenderId);
            var isRecipientExists = _userRepository.GetAll().Any(u => u.Id == messageDto.RecipientId);
            var senderSocialProfile = _socialProfileRepository.Get(messageDto.SenderId);
            var isFollower = senderSocialProfile.IsFollower(messageDto.RecipientId);

            return isSenderExists && isRecipientExists && isFollower;
        }
        public Result<MessageDto> MarkAsRead(int id)
        {
            var readMessage = _messageRepository.MarkAsRead(id);
            return MapToDto(readMessage);
        }
    }
}
