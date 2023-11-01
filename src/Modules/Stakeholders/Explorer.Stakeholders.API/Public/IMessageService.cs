using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IMessageService
    {
        Result<PagedResult<MessageDto>> GetPaged(int page, int pageSize);
        Result<MessageDto> Get(int id);
        Result<MessageDto> Create(MessageDto message);
        Result<MessageDto> Update(MessageDto message);
        Result Delete(int id);
        Result<MessageDto> MarkAsRead(int messageId);
        Result<List<MessageDto>> GetNotifications(int userId);
    }
}
