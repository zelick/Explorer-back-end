using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain;

public class BlogComment : Entity
{
    public long UserId { get; init; }
    public long BlogPostId { get; init; }
    public TimeOnly CreationTime { get; init; }
    public TimeOnly ModificationTime { get; set; }
    public string Text { get; set; }

    public BlogComment(long userId, long blogPostId, TimeOnly creationTime, TimeOnly modificationTime, string text)
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