using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class SocialProfile : Entity
    {
        public long UserId { get; private set; }
        public List<User> Followers { get; set; } = new List<User>();
        public List<User> Followed { get; set; } = new List<User>();

        public SocialProfile() { }

        public SocialProfile(long userId)
        {
            UserId = userId;
            Followers = new List<User>();
            Followed = new List<User>();
        }

        public void Follow(User user)
        {
            Followed.Add(user);
        }

        public bool IsFollower(long recipientId)
        {
            return Followers.Any(u => u.Id == recipientId);
        }
    }
}
