using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain;

public class BlogPost : Entity
{
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime CreationDate { get; init; }
    public List<string>? ImageUrls { get; init; }
    public BlogPostStatus Status { get; init; }


    public BlogPost(string title, string description, DateTime creationDate, List<string?> imageUrls, BlogPostStatus status)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid Title.");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
        Title = title;
        Description = description;
        CreationDate = creationDate;
        ImageUrls = imageUrls;
        Status = status;
    }
}

public enum BlogPostStatus
{
    Draft,
    Published,
    Closed
}