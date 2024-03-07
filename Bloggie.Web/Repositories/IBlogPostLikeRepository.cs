using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<IEnumerable<BlogPostLike1>> GetLikesForBlog(Guid blogPostId);
        Task<BlogPostLike1> AddLikeForBlog(BlogPostLike1 blogPostLike1);
    }
}
