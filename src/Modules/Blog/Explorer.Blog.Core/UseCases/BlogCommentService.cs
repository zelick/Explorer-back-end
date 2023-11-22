using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.BlogPosts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.Core.UseCases;

public class BlogCommentService : BaseService<BlogPostDto, BlogPost>, IBlogCommentService
{
    private readonly IBlogPostRepository _blogPostsRepository;
    private readonly IMapper _mapper;

    public BlogCommentService(IBlogPostRepository repository, IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
        _blogPostsRepository = repository;
    }

    public Result<BlogPostDto> Add(int blogPostId, BlogCommentDto blogCommentDto)
    {
        try
        {
            var blogPost = _blogPostsRepository.Get(blogPostId);
            var blogComment = _mapper.Map<BlogCommentDto, BlogComment>(blogCommentDto);

            blogPost.AddComment(blogComment);
            var result = _blogPostsRepository.Update(blogPost);
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

    public Result Remove(int blogPostId, BlogCommentDto blogCommentDto)
    {
        try
        {
            var blogPost = _blogPostsRepository.Get(blogPostId);
            var blogComment = _mapper.Map<BlogCommentDto, BlogComment>(blogCommentDto);

            blogPost.DeleteComment(blogComment);
            _blogPostsRepository.Update(blogPost);
            return Result.Ok();
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
}