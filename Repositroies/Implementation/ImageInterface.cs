using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using myproject.Data;
using myproject.Models.Domain;
using myproject.Repositroies.Interface;

namespace myproject.Repositroies.Implimentation
{
    public class ImageInterface : IImageInterface
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;
        public ImageInterface(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor
            ,ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogImages>> getAll()
        {
            return await dbContext.blogImages.ToListAsync();
        }

        public async Task<BlogImages> uploadImage(IFormFile file, BlogImages blogImages)
        {

            //upload in local repository api/images
            var localpath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Images", $"{blogImages.fileName}{blogImages.fileExtension}");
            using var stream = new FileStream(localpath, FileMode.Create);
            await file.CopyToAsync(stream);

            //update to database
            //https://codedb.com/images/somefile.jpg

            HttpRequest httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImages.fileName}{blogImages.fileExtension}";
            blogImages.url = urlPath;
            await dbContext.AddAsync(blogImages);
            await dbContext.SaveChangesAsync();
            return blogImages;
        }
    }
}
