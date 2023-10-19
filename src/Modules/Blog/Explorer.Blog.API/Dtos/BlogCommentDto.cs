namespace Explorer.Blog.API.Dtos;

public class BlogCommentDto
{
    public int Id { get; set; }
    public long UserId { get; init; }
    public long BlogPostId { get; init; }
    public DateTime CreationTime { get; init; }
    public DateTime? ModificationTime { get; set; }
    public string Text { get; set; }
}