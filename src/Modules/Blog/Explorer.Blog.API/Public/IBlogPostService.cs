using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public;

public interface IBlogPostService
{
    Result<PagedResult<BlogPostDto>> GetByUser(int page, int pagedSize, long id);
    Result<PagedResult<BlogPostDto>> GetPaged(int page, int pageSize);
    Result<BlogPostDto> Create(BlogPostDto blogPost);
    Result<BlogPostDto> Update(BlogPostDto blogPost);
    Result Delete(int id);
}