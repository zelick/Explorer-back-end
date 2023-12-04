using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IVerificationService
    {
        Result Verify(string verificationTokenData);
        Result<bool> IsUserVerified(string username);
        Result<VerificationTokenDto> createVerificationToken(int userId);
    }
}
