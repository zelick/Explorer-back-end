using Explorer.Blog.API.Public;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;

namespace Explorer.Blog.Core.UseCases;

public class BlogCommentService : CrudService<BlogCommentDto, BlogComment>, IBlogCommentService
{
    public BlogCommentService(ICrudRepository<BlogComment> repository, IMapper mapper) : base(repository, mapper) { }
}