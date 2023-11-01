using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Message : Entity
    {
        public int SenderId { get; private set; }
        public int RecipientId { get; private set; }
        public DateTime? SentDateTime { get; private set; }
        public DateTime? ReadDateTime { get; private set; }
        public string Content { get; private set; }
        public bool IsRead { get; private set; }

        public Message(int senderId, int recipientId, DateTime? sentDateTime, DateTime? readDateTime, string content, bool isRead)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            SentDateTime = DateTime.UtcNow;
            ReadDateTime = null;
            Content = content;
            IsRead = false;
            Validate();
        }

        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(Content)) throw new ArgumentException("Invalid message content");
        }

        public void MarkAsRead()
        {
            ReadDateTime = DateTime.UtcNow;
            IsRead = true;
        }

    }
}
