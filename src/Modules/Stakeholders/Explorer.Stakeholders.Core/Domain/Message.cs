using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Message : Entity
    {
        public long SenderId { get; private set; }
        public long RecipientId { get; private set; }
        public string Title { get; private set; }
        public DateTime? SentDateTime { get; private set; }
        public DateTime? ReadDateTime { get; private set; }
        public string Content { get; private set; }
        public bool IsRead { get; private set; }

        public Message(long senderId, long recipientId, string title, DateTime? sentDateTime, DateTime? readDateTime, string content, bool isRead)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            Title = title;
            SentDateTime = SentDateTime = sentDateTime ?? DateTime.UtcNow;
            ReadDateTime = null;
            Content = content;
            IsRead = false;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid message title.");
            if (string.IsNullOrWhiteSpace(Content)) throw new ArgumentException("Invalid message content.");
        }

        public void MarkAsRead()
        {
            ReadDateTime = DateTime.UtcNow;
            IsRead = true;
        }

    }
}
