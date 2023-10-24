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

    public Result<PagedResult<BlogPostDto>> GetByUser(int page, int pagedSize, int id)
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

    public override Result<BlogPostDto> Update(BlogPostDto blogPostDto)
    {
        try
        {
            var blogPost = CrudRepository.Get(blogPostDto.Id);

            if (blogPost.Status != BlogPostStatus.Draft) throw new ArgumentException("Only Blog Posts with Draft Status can be updated.");

            blogPost.Update(MapToDomain(blogPostDto));
            var result = CrudRepository.Update(blogPost);
            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<BlogPostDto> Close(int id)
    {
        try
        {
            var blogPost = _blogPostsRepository.Get(id);
            blogPost.Close();
            var result = _blogPostsRepository.Update(blogPost);
            return MapToDto(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}