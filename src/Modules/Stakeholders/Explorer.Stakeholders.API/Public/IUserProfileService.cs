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
        void Follow(int userId, int followedUserId);
        Result<UserProfileDto> Get(int userId);
    }
}
