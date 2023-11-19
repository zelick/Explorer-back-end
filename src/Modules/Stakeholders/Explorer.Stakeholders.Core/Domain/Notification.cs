using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Stakeholders.Core.Domain
{
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
        // if Type == OTHER             -> ForeignId = null
        // if Type == REQUEST           -> ForeignId = RequestId
        // if Type == REPORTED_ISSUE    -> ForeignId = ReportedIssueId

        public Notification() { }
        public Notification(string description, DateTime creationTime, NotificationType type,bool isRead, long userId, long? foreignId)
        {
            Description = description;
            CreationTime = creationTime;
            Type = type;
            IsRead = isRead;
            UserId = userId;
            ForeignId = foreignId;

            ValidateNotification();
        }

        private void ValidateNotification()
        {
            if (string.IsNullOrWhiteSpace(Description) || UserId == 0) 
                throw new ArgumentException("Invalid notification.");
            if (string.IsNullOrWhiteSpace(Type.ToString())) 
                throw new ArgumentException("Invalid notification type.");
        }
    }
}
public enum NotificationType
{
    OTHER,
    REQUEST,
    REPORTED_ISSUE
}
