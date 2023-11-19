using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsRead { get; set; }
        public long UserId { get; set; }
        public UserDto? User { get; set; }
        public NotificationType Type { get; set; }
        public long? ForeignId { get; set; }
    }

    public enum NotificationType
    {
        OTHER,
        REQUEST,
        REPORTED_ISSUE
    }
}
