namespace Explorer.Stakeholders.API.Dtos
{
    public class MessageDto
    {
        public long? Id { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string SenderUsername { get; set; }
        public string Title { get; set; }
        public DateTime? SentDateTime { get;  set; }
        public DateTime? ReadDateTime { get;  set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

    }
}
