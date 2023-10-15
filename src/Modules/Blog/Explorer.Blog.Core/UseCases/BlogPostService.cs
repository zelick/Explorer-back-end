using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.UseCases;

public class BlogPostService : CrudService<BlogPostDto, BlogPost>, IBlogPostService
{
    public BlogPostService(ICrudRepository<BlogPost> repository, IMapper mapper) : base(repository, mapper) { }

}