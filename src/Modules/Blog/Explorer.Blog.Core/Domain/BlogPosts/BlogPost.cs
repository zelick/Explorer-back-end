using System.Data;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain.BlogPosts;

public class BlogPost : Entity
{
    private const int ClosedRatingThreshold = -10;
    private const int ActiveRatingThreshold = 100;
    private const int FamousRatingThreshold = 500;
    private const int FamousCommentThreshold = 10;
    private const int ActiveCommentThreshold = 30;

    public long UserId { get; init; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreationDate { get; private set; }
    public List<string>? ImageUrls { get; private set; }
    public BlogPostStatus Status { get; private set; }
    public List<BlogRating>? Ratings { get; private set; }
    public List<BlogComment>? Comments { get; private set; }

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
        Comments = new List<BlogComment>();
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
        if (Status == BlogPostStatus.Closed)
            throw new ArgumentException("Closed blogs are read-only!");

        Ratings ??= new List<BlogRating>();

        var existingRating = Ratings.FirstOrDefault(rating => rating?.UserId == blogRating.UserId);
        if (existingRating is not null)
        {
            if (existingRating.Rating == blogRating.Rating)
            {
                Ratings.Remove(existingRating);
            }
            else
            {
                existingRating.Rating = blogRating.Rating;
                existingRating.TimeStamp = DateTime.Now;
            }
        }
        else
        {
            Ratings.Add(blogRating);
        }

        UpdateStatus();
    }

    public void AddComment(BlogComment blogComment)
    {
        if (Status == BlogPostStatus.Closed)
            throw new ArgumentException("Closed blogs are read-only.");

        Comments ??= new List<BlogComment>();

        var existingComment = Comments.FirstOrDefault(c => c?.UserId == blogComment.UserId && c?.CreationTime == blogComment.CreationTime);
        if (existingComment is not null)
        {
            existingComment.ModificationTime = DateTime.Now;
            existingComment.Text = blogComment.Text;
        }
        else
        {
            Comments.Add(blogComment);
        }
    }

    public void DeleteComment(BlogComment blogComment)
    {
        if (Status == BlogPostStatus.Closed)
            throw new ArgumentException("Closed blogs are read-only.");

        Comments ??= new List<BlogComment>();

        var existingComment = Comments.FirstOrDefault(c => c?.UserId == blogComment.UserId && c?.CreationTime == blogComment.CreationTime);
        if (existingComment is null)
            throw new ArgumentException("Comment not found.");

        Comments.Remove(existingComment);
    }

    public void UpdateStatus()
    {
        Ratings ??= new List<BlogRating>();
        Comments ??= new List<BlogComment>();

        var upvoteCount = Ratings.Count(r => r?.Rating == Rating.Upvote);
        var downvoteCount = Ratings.Count(r => r?.Rating == Rating.Downvote);
        var rating = upvoteCount - downvoteCount;
        var commentCount = Comments.Count;

        if (rating < ClosedRatingThreshold)
        {
            Status = BlogPostStatus.Closed;
        }
        else if (rating > FamousRatingThreshold && commentCount > FamousCommentThreshold)
        {
            Status = BlogPostStatus.Famous;
        }
        else if (rating > ActiveRatingThreshold || commentCount > ActiveCommentThreshold)
        {
            Status = BlogPostStatus.Active;
        }
    }
}

public enum BlogPostStatus
{
    Draft,
    Published,
    Closed,
    Active,
    Famous
}