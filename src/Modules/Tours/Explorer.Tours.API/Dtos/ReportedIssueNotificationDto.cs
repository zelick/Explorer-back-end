namespace Explorer.Tours.API.Dtos
{
    public class ReportedIssueNotificationDto
    {
        public int Id { get; set; }
        public string NotificationDescription { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsRead { get; set; }
        public int UserId { get; set; }
        public int ReportedIssueId { get; set; }
        public ReportedIssueDto ReportedIssue { get; set; }
    }
}
