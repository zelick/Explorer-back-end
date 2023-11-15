using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IMessageService
    {
        Result<PagedResult<MessageDto>> GetPaged(int page, int pageSize);
        Result<MessageDto> Get(int id);
        Result<MessageDto> Create(MessageDto messageDto);
        Result<MessageDto> Update(MessageDto messageDto);
        Result Delete(int id);
        Result<MessageDto> Send(MessageDto messageDto);
        Result<List<MessageDto>> GetAllSent(int userId);
        Result<List<MessageDto>> GetAllReceived(int userId);
        Result<List<MessageDto>> GetAllUnread(int userId);
        Result<MessageDto> MarkAsRead(int id);
    }
}
