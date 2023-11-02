using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public;

public interface IBlogPostService
{
    Result<PagedResult<BlogPostDto>> GetAllNonDraft(int page, int pageSize);
    Result<PagedResult<BlogPostDto>> GetAllByUser(int page, int pageSize, int id);
    Result<BlogPostDto> Get(int id);
    Result<BlogPostDto> Create(BlogPostDto blogPost);
    Result<BlogPostDto> Update(BlogPostDto blogPost);
    Result<BlogPostDto> Close(int id);
    Result Delete(int id);
}