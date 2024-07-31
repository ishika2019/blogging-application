using Microsoft.EntityFrameworkCore;
using myproject.Data;
using myproject.Models.Domain;
using myproject.Repositroies.Interface;

namespace myproject.Repositroies.Implimentation
{
    public class BlogPostInterface : IBlogPostInterface
    {
        private readonly ApplicationDbContext dbContext;
        public BlogPostInterface(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            dbContext.BlogPosts.AddAsync(blogPost);
            dbContext.SaveChanges();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogByIdAsync(Guid id)
        {
            var existing =await dbContext.BlogPosts.FirstOrDefaultAsync(x=>x.Id == id);
            if (existing == null)
            {
                return null;

            }
            dbContext.BlogPosts.Remove(existing);
            dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<IEnumerable<BlogPost>> getAsync()
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> getBlogByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> getBlogByUrlHandleAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> updateBlogAsync(BlogPost blogPost)
        {
           var existing=await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existing == null)
            {
                return null;
            }
            dbContext.Entry(existing).CurrentValues.SetValues(blogPost);
            existing.Categories=blogPost.Categories;
            dbContext.SaveChangesAsync();
            return blogPost;
        }
    }
}
