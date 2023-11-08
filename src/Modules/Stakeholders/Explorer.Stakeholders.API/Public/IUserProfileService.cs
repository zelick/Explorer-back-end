using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserProfileService
    {
        Result<UserProfileDto> Follow(int userId, int followedUserId);
        Result<UserProfileDto> Get(int userId);
        Result<MessageDto> SendMessage(MessageDto  message);
        Result<MessageDto> MarkAsRead(int messageId);
        Result<List<MessageDto>> GetNotifications(int userId);
    }
}
