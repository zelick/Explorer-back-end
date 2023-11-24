using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IVerificationTokenRepository
    {
        VerificationToken CreateVerificationToken(long userId);
        VerificationToken GetByUserId(long userId);
        VerificationToken GetByTokenData(string tokenData);
    }
}
