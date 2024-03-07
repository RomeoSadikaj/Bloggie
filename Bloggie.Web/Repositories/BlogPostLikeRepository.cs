
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike1> AddLikeForBlog(BlogPostLike1 blogPostLike1)
        {
            await bloggieDbContext.BlogPostsLike1.AddAsync(blogPostLike1);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike1;
        }

        public async Task<IEnumerable<BlogPostLike1>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostsLike1.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostsLike1
                .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
