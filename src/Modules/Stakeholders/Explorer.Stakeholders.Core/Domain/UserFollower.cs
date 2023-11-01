using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class UserFollower
    {
        public int UserId { get; private set; }
        public int FollowerId { get; private set; }

        public UserFollower () { }

        public UserFollower(int userId, int followerId)
        {
            UserId = userId;
            FollowerId = followerId;
        }
    }
}
