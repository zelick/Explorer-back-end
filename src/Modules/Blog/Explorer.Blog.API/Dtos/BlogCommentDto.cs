namespace Explorer.Blog.API.Dtos;

public class BlogCommentDto
{
    public int Id { get; set; }
    public long UserId { get; init; }
    public long BlogPostId { get; init; }
    public TimeOnly CreationTime { get; init; }
    public TimeOnly ModificationTime { get; set; }
    public string Text { get; set; }
}