using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Blog.Core.Domain.BlogPosts;

public class BlogComment : ValueObject
{
    public long UserId { get; init; }
    public DateTime CreationTime { get; init; }
    public DateTime? ModificationTime { get; set; }
    public string Text { get; set; }

    public BlogComment() { }

    [JsonConstructor]
    public BlogComment(long userId, DateTime creationTime, DateTime? modificationTime, string text)
    {
        UserId = userId;
        CreationTime = creationTime;
        ModificationTime = modificationTime;
        Text = text;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Text)) throw new ArgumentException("Invalid Text");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return CreationTime;
        yield return Text;
    }
}