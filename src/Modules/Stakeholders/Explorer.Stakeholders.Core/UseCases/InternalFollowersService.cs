using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalFollowersService : IInternalFollowersService
    {
        private readonly ISocialProfileRepository _socialProfileRepository;

        public InternalFollowersService(ISocialProfileRepository socialProfileRepository)
        {
            _socialProfileRepository = socialProfileRepository;
        }
        public List<long> GetFollowerIds(long loggedId)
        {
            var socialProfile = _socialProfileRepository.Get(loggedId);
            var followerIds = new List<long>();

            foreach (var follower in socialProfile.Followers)
            {
                followerIds.Add(follower.Id);
            }

            return followerIds;
        }
    }
}
