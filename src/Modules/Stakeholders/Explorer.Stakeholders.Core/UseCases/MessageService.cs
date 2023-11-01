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

        public MessageService(ICrudRepository<Message> repository, IMapper mapper, IMessageRepository messageRepository)
            : base(repository, mapper)
        {
            _messageRepository = messageRepository;
        }

        public Result<MessageDto> MarkAsRead(int messageId)
        {
            var message = _messageRepository.MarkAsRead(messageId);

            return MapToDto(message);
        }

        public Result<List<MessageDto>> GetNotifications(int userId)
        {
            var notifications = _messageRepository.GetAllUnread(userId);

            return MapToDto(notifications);
        }
    }
}
