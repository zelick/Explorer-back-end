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
        var items = PageResults(page, pageSize, query);

        return new PagedResult<BlogPost>(items, count);
    }

    public PagedResult<BlogPost> GetAllByUser(int page, int pageSize, long userId)
    {
        var query = DbContext.BlogPosts.Where(bp => bp.UserId == userId);
        var count = query.Count();
        var items = PageResults(page, pageSize, query);

        return new PagedResult<BlogPost>(items, count);
    }

    public PagedResult<BlogPost> GetFilteredByStatus(int page, int pageSize, BlogPostStatus status)
    {
        var query = DbContext.BlogPosts.Where(bp => bp.Status == status);
        var count = query.Count();
        var items = PageResults(page, pageSize, query);

        return new PagedResult<BlogPost>(items, count);
    }

    // BUG: paging doesn't work when filtering
    private List<BlogPost> PageResults(int page, int pageSize, IQueryable<BlogPost> query)
    {
        if (pageSize != 0 && page != 0)
        {
            return query.OrderByDescending(bp => bp.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        return query.ToList();
    }
}
