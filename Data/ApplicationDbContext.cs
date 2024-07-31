using Microsoft.EntityFrameworkCore;
using myproject.Models.Domain;

namespace myproject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext
            >options) : base(options)
        {
        }

       public  DbSet<BlogPost> BlogPosts { get; set; }
       public  DbSet<Category> Categories { get; set; }
        public DbSet<BlogImages> blogImages { get; set; }
    }
}
