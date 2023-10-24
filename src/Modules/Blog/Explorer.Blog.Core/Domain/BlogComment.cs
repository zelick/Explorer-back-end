using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain;

public class BlogComment : Entity
{
    public long UserId { get; init; }
    public long BlogPostId { get; init; }
    public DateTime CreationTime { get; init; }
    public DateTime? ModificationTime { get; set; }
    public string Text { get; set; }

    public BlogComment(long userId, long blogPostId, DateTime creationTime, DateTime? modificationTime, string text)
    {
        UserId = userId;
        BlogPostId = blogPostId;
        CreationTime = creationTime;
        ModificationTime = modificationTime;
        Text = text;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Text)) throw new ArgumentException("Invalid Text");
    }
}