using Explorer.Blog.API.Dtos;
using FluentResults;

namespace Explorer.Blog.API.Public;

public interface IBlogCommentService
{
    Result<BlogPostDto> Add(int blogPostId, BlogCommentDto blogCommentDto);
    Result Remove(int blogPostId, BlogCommentDto blogCommentDto);
}