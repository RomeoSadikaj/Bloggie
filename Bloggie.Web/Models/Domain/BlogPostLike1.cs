namespace Bloggie.Web.Models.Domain
{
    public class BlogPostLike1
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
