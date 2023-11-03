namespace Explorer.Stakeholders.API.Dtos
{
    public class MessageDto
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime? SentDateTime { get;  set; }
        public DateTime? ReadDateTime { get;  set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

    }
}
