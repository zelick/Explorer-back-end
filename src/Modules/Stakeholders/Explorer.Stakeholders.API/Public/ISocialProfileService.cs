using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface ISocialProfileService
    {
        Result<SocialProfileDto> Follow(int followerId, int followedUserId);
        Result<SocialProfileDto> UnFollow(int followerId, int unFollowedUserId);
        Result<SocialProfileDto> Get(int userId);
    }
}
