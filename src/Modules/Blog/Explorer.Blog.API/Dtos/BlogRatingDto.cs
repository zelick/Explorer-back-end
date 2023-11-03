namespace Explorer.Blog.API.Dtos;

public class BlogRatingDto
{
    public int UserId { get; set; }
    public string Rating { get; set; }
    public DateTime TimeStamp { get; set; }
}