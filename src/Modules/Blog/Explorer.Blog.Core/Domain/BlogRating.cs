using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain;

public class BlogRating : ValueObject
{
    public long UserId { get; init; }
    public DateTime TimeStamp { get; set; }
    public Rating Rating { get; set; }

    public BlogRating() { }

    [JsonConstructor]
    public BlogRating(long userId, DateTime timeStamp, Rating rating)
    {
        UserId = userId;
        TimeStamp = timeStamp;
        Rating = rating;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return TimeStamp;
        yield return Rating;
    }
}

public enum Rating
{
    Upvote,
    Downvote
}
