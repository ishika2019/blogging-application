using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using myproject.Models.Domain;
using myproject.Models.DTO;
using myproject.Repositroies.Interface;

namespace myproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostInterface bi;
        private readonly ICategoryInterface ci;
        public BlogPostController(IBlogPostInterface bi, ICategoryInterface ci)
        {
            this.bi = bi;
            this.ci = ci;
        }
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostRequestDto request)
        {
            //request to domain
            var blogpost = new BlogPost
            {
                Title = request.Title,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImage = request.FeaturedImage,
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existing = await ci.getByIdAsync(categoryGuid);
                if (existing != null)
                {
                    blogpost.Categories.Add(existing);
                }
            }
            blogpost = await bi.CreateAsync(blogpost);

            //domain to dto
            var reponse = new BlogPostDto
            {
                Id = blogpost.Id,
                Title = request.Title,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImage = request.FeaturedImage,
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = blogpost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),

            };
            return Ok(reponse);
        }
        [HttpGet]
        public async Task<IActionResult> getAllBlogs()
        {
            var blog = await bi.getAsync();
            var response = new List<BlogPostDto>();
            foreach (var item in blog)
            {
                response.Add(new BlogPostDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content,
                    ShortDescription = item.ShortDescription,
                    FeaturedImage = item.FeaturedImage,
                    UrlHandle = item.UrlHandle,
                    PublishDate = item.PublishDate,
                    Author = item.Author,
                    IsVisible = item.IsVisible,
                    Categories = item.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList(),


                });
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getBlogById([FromRoute] Guid id)
        {
            var item = await bi.getBlogByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var response = new BlogPostDto
            {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content,
                ShortDescription = item.ShortDescription,
                FeaturedImage = item.FeaturedImage,
                UrlHandle = item.UrlHandle,
                PublishDate = item.PublishDate,
                Author = item.Author,
                IsVisible = item.IsVisible,
                Categories = item.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),

            };
            return Ok(response);
        }



        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> getBlogByUrlHandle([FromRoute] string urlHandle)
        {
            var item = await bi.getBlogByUrlHandleAsync(urlHandle);
            if (item == null)
            {
                return NotFound();
            }
            var response = new BlogPostDto
            {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content,
                ShortDescription = item.ShortDescription,
                FeaturedImage = item.FeaturedImage,
                UrlHandle = item.UrlHandle,
                PublishDate = item.PublishDate,
                Author = item.Author,
                IsVisible = item.IsVisible,
                Categories = item.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),

            };
            return Ok(response);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> updateBlogById([FromRoute] Guid id,UpdateBlogPostRequestDto request)
        {
            var blogpost = new BlogPost
            {
                Id= id,
                Title = request.Title,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImage = request.FeaturedImage,
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };
            foreach (var categoryGuid in request.Categories)
            {
                var existing = await ci.getByIdAsync(categoryGuid);
                if (existing != null)
                {
                    blogpost.Categories.Add(existing);
                }
            }

            var updatedblogpost=await bi.updateBlogAsync(blogpost);
            var response = new BlogPostDto
            {
                Id = updatedblogpost.Id,
                Title = updatedblogpost.Title,
                Content = updatedblogpost.Content,
                ShortDescription = updatedblogpost.ShortDescription,
                FeaturedImage = updatedblogpost.FeaturedImage,
                UrlHandle = updatedblogpost.UrlHandle,
                PublishDate = updatedblogpost.PublishDate,
                Author = updatedblogpost.Author,
                IsVisible = updatedblogpost.IsVisible,
                Categories = updatedblogpost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),
            };
            return Ok(response);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> deleteBlog(Guid id)
        {
            var existing=await bi.DeleteBlogByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }
            var response=new BlogPostDto
            {
                Id = existing.Id,
                Title = existing.Title,
                Content = existing.Content,
                ShortDescription = existing.ShortDescription,
                FeaturedImage = existing.FeaturedImage,
                UrlHandle = existing.UrlHandle,
                PublishDate = existing.PublishDate,
                Author = existing.Author,
                IsVisible = existing.IsVisible,
               
            };
            return Ok(response);


        }
     }
}
