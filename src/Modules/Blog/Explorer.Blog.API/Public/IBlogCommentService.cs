using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public;

public interface IBlogCommentService
{
    Result<PagedResult<BlogCommentDto>> GetPaged(int page, int pageSize);
    Result<BlogCommentDto> Create(BlogCommentDto comment);
}