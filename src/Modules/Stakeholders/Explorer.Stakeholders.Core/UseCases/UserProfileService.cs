using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;

        public UserProfileService(IUserFollowerRepository userFollowerRepository)
        {
            _userFollowerRepository = userFollowerRepository;
        }

        public void Follow(int userId, int followedUserId)
        {
            _userFollowerRepository.AddFollower(userId, followedUserId);
        }
    }
}
