using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain;

public class BlogPost : Entity
{
    public long UserId { get; init; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreationDate { get; private set; }
    public List<string>? ImageUrls { get; private set; }
    public BlogPostStatus Status { get; private set; }
    public List<BlogRating>? Ratings { get; private set; }

    public BlogPost(long userId, string title, string description, DateTime creationDate, List<string>? imageUrls,
        BlogPostStatus status)
    {
        UserId = userId;
        Title = title;
        Description = description;
        CreationDate = creationDate;
        ImageUrls = imageUrls;
        Status = status;
        Ratings = new List<BlogRating>();
        Validate();
    }

    public void Update(BlogPost updatedBlogPost) 
    {
        Title = updatedBlogPost.Title;
        Description = updatedBlogPost.Description;
        CreationDate = updatedBlogPost.CreationDate;
        ImageUrls = updatedBlogPost.ImageUrls;
        Status = updatedBlogPost.Status;
    }

    public void Close()
    {
        if (Status != BlogPostStatus.Published) throw new ArgumentException("Invalid Status");

        Status = BlogPostStatus.Closed;
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid Title.");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description.");
        if (CreationDate.Date > DateTime.Now.Date) throw new ArgumentException("Invalid CreationDate.");
    }

    public void AddRating(BlogRating blogRating)
    {
        Ratings ??= new List<BlogRating>();

        var existingRating = Ratings.FirstOrDefault(rating => rating.UserId == blogRating.UserId);

        if (existingRating is not null)
        {
            if (existingRating.Rating == blogRating.Rating)
                throw new ArgumentException("Cannot vote with the same rating again.");

            existingRating.Rating = blogRating.Rating;
            existingRating.TimeStamp = DateTime.Now;
        }
        else
        {
            Ratings.Add(blogRating);
        }
    }
}

public enum BlogPostStatus
{
    Draft,
    Published,
    Closed
}