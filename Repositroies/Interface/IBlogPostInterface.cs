using myproject.Models.Domain;

namespace myproject.Repositroies.Interface
{
    public interface IBlogPostInterface
    {

       Task<BlogPost> CreateAsync(BlogPost blogPost);
       Task<IEnumerable<BlogPost>> getAsync();
        Task<BlogPost?> getBlogByIdAsync(Guid id);
        Task<BlogPost?> getBlogByUrlHandleAsync(string urlHandle);
        Task<BlogPost?> updateBlogAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteBlogByIdAsync(Guid id);
    }
}
