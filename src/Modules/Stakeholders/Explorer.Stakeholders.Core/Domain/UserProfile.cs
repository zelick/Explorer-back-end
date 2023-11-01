using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class UserProfile
    {
        public User User { get; private set; }
        public List<User> Followers { get; private set; }
        public List<Message> Messages { get; private set; }
        public List<Message> UnreadNotification { get; private set; }

        public UserProfile(User user, List<User> followers, List<Message> messages, List<Message> unreadNotification)
        {
            User = user;
            Followers = followers; 
            Messages = messages;
            UnreadNotification = unreadNotification;
        }
    }
}
