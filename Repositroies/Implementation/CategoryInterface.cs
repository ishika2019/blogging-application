using Microsoft.EntityFrameworkCore;
using myproject.Data;
using myproject.Models.Domain;
using myproject.Repositroies.Interface;

namespace myproject.Repositroies.Implimentation
{
    public class CategoryInterface : ICategoryInterface
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryInterface( ApplicationDbContext dbContext)
        {
            this.dbContext= dbContext;
        }

        

        public  async Task<Category> CreatAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> deleteAsync(Guid id)
        {
            var existingCategory=await dbContext.Categories.FirstOrDefaultAsync(c => c.Id==id);
            if (existingCategory==null) {
                return null;
            }
            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public  async Task<IEnumerable<Category>> getAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> getByIdAsync(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpadteAsync(Category category)
        {
            var existingcategory= await dbContext.Categories.FirstOrDefaultAsync(x=>x.Id == category.Id);
            if (existingcategory != null)
            {
                dbContext.Entry(existingcategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }


    }
}
