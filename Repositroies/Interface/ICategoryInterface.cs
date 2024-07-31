using myproject.Models.Domain;

namespace myproject.Repositroies.Interface
{
    public interface ICategoryInterface
    {
        Task<Category> CreatAsync(Category category);
        Task<IEnumerable<Category>> getAllAsync();
        Task<Category?> getByIdAsync(Guid id);
        Task<Category?> UpadteAsync(Category category);

        Task<Category?> deleteAsync(Guid id);



    }
}
