namespace Explorer.Blog.API.Dtos;

public class BlogCommentDto
{
    public int UserId { get; init; }
    public string? Username { get; set; }
    public DateTime CreationTime { get; init; }
    public DateTime? ModificationTime { get; set; }
    public string Text { get; set; }
}