using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface ISecureTokenService
    {
        Result<SecureTokenDto> CreateSecureToken(long userId);
        Result<SecureTokenDto> GetByUserUsername(string username);
        Result<SecureTokenDto> UseSecureToken(long tokenId);
    }
}
