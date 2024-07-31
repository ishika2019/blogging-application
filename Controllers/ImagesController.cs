using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myproject.Models.Domain;
using myproject.Models.DTO;
using myproject.Repositroies.Interface;

namespace myproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageInterface ImageInterface;
        public ImagesController(IImageInterface imageInterface)
        {
            ImageInterface = imageInterface;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var blogimages = await ImageInterface.getAll();
            var response = new List<BlogImageDto>();
            foreach (var blogimage in blogimages)
            {
                response.Add(
                    new BlogImageDto
                    {
                        id = blogimage.id,
                        title = blogimage.title,
                        dateCreated = blogimage.dateCreated,
                        fileExtension = blogimage.fileExtension,
                        fileName = blogimage.fileName,
                        url = blogimage.url
                    }
                ); 
            }
            return  Ok( response );
        }
        

        [HttpPost]
        public async  Task<IActionResult> uploadImages([FromForm] IFormFile file,
           [FromForm] string filename, [FromForm] string title)
        {
            validateFileUpload(file);
            if(ModelState.IsValid)
            {
                var blogImage = new BlogImages
                {
                    fileExtension = Path.GetExtension(file.FileName).ToLower(),
                    fileName = filename,
                    title = title,
                    dateCreated = DateTime.Now
                };

                blogImage= await ImageInterface.uploadImage(file, blogImage);
                var response = new BlogImageDto
                {
                    id = blogImage.id,
                    title = blogImage.title,
                    dateCreated = blogImage.dateCreated,
                    fileExtension = blogImage.fileExtension,
                    fileName = blogImage.fileName,
                    url = blogImage.url,
                };
                return Ok(response);
            }
            return BadRequest(ModelState);

        }
        private void validateFileUpload(IFormFile file)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower())) 
            {
                ModelState.AddModelError("file", "Unsupported file Format");
            }
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size can not be more than 10MB");
            }
        }
    }
}
