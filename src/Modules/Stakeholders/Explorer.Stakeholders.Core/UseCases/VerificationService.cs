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
        public VerificationService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public Result Verify(string verificationToken)
        {
            var user = _userRepository.GetByVerificationToken(verificationToken);

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
