using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ISecureTokenRepository
    {
        SecureToken CreateSecureToken(long userId);
        SecureToken GetByUserId(long userId);
        SecureToken UseSecureToken(long tokenId);
        long GetUserIdBySecureToken(string tokenData);
    }
}
