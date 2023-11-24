using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IAuthenticationService
{
   // Result EditProfile(AccountEditingDto account);
    Result<AuthenticationTokensDto> Login(CredentialsDto credentials);
    Result<AccountRegistrationDto> RegisterTourist(AccountRegistrationDto account);
}