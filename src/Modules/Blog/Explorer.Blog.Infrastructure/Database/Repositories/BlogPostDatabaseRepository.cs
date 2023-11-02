using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;

namespace Explorer.Blog.Infrastructure.Database.Repositories;

public class BlogPostDatabaseRepository : CrudDatabaseRepository<BlogPost, BlogContext>, IBlogPostRepository
{
    public BlogPostDatabaseRepository(BlogContext blogContext) : base(blogContext) {}

    public PagedResult<BlogPost> GetAllNonDraft(int page, int pageSize)
    {
        var query = DbContext.BlogPosts.Where(bp => bp.Status != BlogPostStatus.Draft);

        var count = query.Count();

        var pagedData = (page != 0 && pageSize != 0)
            ? query.Skip((page - 1) * pageSize).Take(pageSize)
            : query;

        var items = pagedData.ToList();

        return new PagedResult<BlogPost>(items, count);
    }

    public PagedResult<BlogPost> GetAllByUser(int page, int pageSize, long userId)
    {
        var query = DbContext.BlogPosts.Where(bp => bp.UserId == userId);

        var count = query.Count();

        var pagedData = (page != 0 && pageSize != 0)
            ? query.Skip((page - 1) * pageSize).Take(pageSize)
            : query;

        var items = pagedData.ToList();

        return new PagedResult<BlogPost>(items, count);
    }
}
