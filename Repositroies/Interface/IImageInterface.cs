using myproject.Models.Domain;
using System.Net;

namespace myproject.Repositroies.Interface
{
    public interface IImageInterface
    {
      Task<BlogImages> uploadImage(IFormFile file,BlogImages blogImages);

        Task<IEnumerable<BlogImages>> getAll();
    }
}
