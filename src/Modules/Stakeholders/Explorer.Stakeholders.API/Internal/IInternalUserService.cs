using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Internal;

public interface IInternalUserService
{
    Result<UserDto> Get(int userId);
}
