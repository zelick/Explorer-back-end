using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class VerificationService : IVerificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationTokenRepository _verificationTokenRepository;
        public VerificationService(IUserRepository userRepository, IVerificationTokenRepository verificationTokenRepository) 
        {
            _userRepository = userRepository;
            _verificationTokenRepository = verificationTokenRepository;
        }

        public Result<VerificationTokenDto> createVerificationToken(int userId)
        {
            throw new NotImplementedException();
        }

        public Result<bool> IsUserVerified(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null) { return Result.Fail("User not found"); }
            return user.IsVerified;
        }

        public Result Verify(string verificationTokenData)
        {
            var token = _verificationTokenRepository.GetByTokenData(verificationTokenData);
            var user = _userRepository.GetUserById(token.UserId);

            if (user == null)
            {
                return Result.Fail("User not found");
            }

            user.IsVerified = true;

            _userRepository.Update(user);
            return Result.Ok();
        }
    }
}
