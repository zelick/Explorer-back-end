using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum NotificationType
    {
        OTHER,
        REQUEST,
        REPORTED_ISSUE,
        MESSAGE
    }

    public class Notification : Entity
    {
        public string Description { get; init; }
        public DateTime CreationTime { get; init; }
        public bool IsRead { get; init; }
        public long UserId { get; init; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public NotificationType Type { get; private set; }
        public long? ForeignId { get; init; }
        // if Type == 0  -> ForeignId = null
        // if Type == 1  -> ForeignId = RequestId
        // if Type == 2  -> ForeignId = ReportedIssueId

        public Notification() { }
        public Notification(string description, DateTime creationTime, NotificationType type, bool isRead, long userId, long? foreignId)
        {
            Description = description;
            CreationTime = creationTime;
            Type = type;
            IsRead = isRead;
            UserId = userId;
            ForeignId = foreignId;

            ValidateNotification();
        }

        public Notification(string description, long userId, long? foreignId, NotificationType type = NotificationType.OTHER)
        {
            Description = description;
            UserId = userId;
            ForeignId = foreignId;
            CreationTime = DateTime.UtcNow;
            IsRead = false;
            Type = type;
            
            ValidateNotification();
        }

        private void ValidateNotification()
        {
            if (string.IsNullOrWhiteSpace(Description) || UserId == 0) 
                throw new ArgumentException("Invalid notification.");
            if (CreationTime.Date > DateTime.Now.Date) 
                throw new ArgumentException("Invalid CreationTime.");

            if (string.IsNullOrWhiteSpace(Type.ToString()))
                throw new ArgumentException("Invalid notification type.");
        }

        public void OtherType() {
            Type = NotificationType.OTHER;
        }

        public void RequestType()
        {
            Type = NotificationType.REQUEST;
        }
        public void ReportedIssueType()
        {
            Type = NotificationType.REPORTED_ISSUE;
        }
        public void MessageType()
        {
            Type = NotificationType.MESSAGE;
        }
    }
}