using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces;

public interface IBlogPostRepository: ICrudRepository<BlogPost>
{
    PagedResult<BlogPost> GetByUser(int page, int pageSize, long userId);
}