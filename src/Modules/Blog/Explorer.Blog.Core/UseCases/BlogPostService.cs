using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Blog.Core.UseCases;

public class BlogPostService : CrudService<BlogPostDto, BlogPost>, IBlogPostService
{
    private readonly IBlogPostRepository _blogPostsRepository;

    public BlogPostService(IBlogPostRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _blogPostsRepository = repository;
    }

    public Result<PagedResult<BlogPostDto>> GetByUser(int page, int pagedSize, long id)
    {
        try
        {
            var result = _blogPostsRepository.GetByUser(page, pagedSize, id);
            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    // TODO Override Update to add Status transition logic
}