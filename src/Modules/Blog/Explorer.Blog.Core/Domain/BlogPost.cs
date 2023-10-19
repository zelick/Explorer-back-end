using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain;

public class BlogPost : Entity
{
    public long UserId { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime CreationDate { get; init; }
    public List<string>? ImageUrls { get; init; }
    public BlogPostStatus Status { get; init; }


    public BlogPost(long userId, string title, string description, DateTime creationDate, List<string>? imageUrls,
        BlogPostStatus status)
    {
        UserId = userId;
        Title = title;
        Description = description;
        CreationDate = creationDate;
        ImageUrls = imageUrls;
        Status = status;
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid Title.");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description.");
    }
}

public enum BlogPostStatus
{
    Draft,
    Published,
    Closed
}