using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using myproject.Data;
using myproject.Models.Domain;
using myproject.Models.DTO;
using myproject.Repositroies.Interface;

namespace myproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryInterface ci;
        public CategoriesController(ICategoryInterface ci)
        {
            this.ci = ci;
        }

       
        [HttpPost]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateCategory(CreatCatagoryRequestDto req)
        {
            //map dto to domain model
            var category = new Category
            {
                Name = req.Name,
                UrlHandle = req.UrlHandle,
            };

            await ci.CreatAsync(category);

            // domain to dto
            var reponse = new CategoryDto
            {
                Id = category.Id,
                Name = req.Name,
                UrlHandle = req.UrlHandle,
            };
            return Ok(category);

        }
        [HttpGet]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> getAllCategories()
        {
            var category = await ci.getAllAsync();
            //map domain model to dto

            var response = new List<CategoryDto>();
            foreach (var item in category)
            {
                response.Add(new CategoryDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    UrlHandle = item.UrlHandle,
                });
            }
            return Ok(response);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getCategoryById(Guid id)
        {
            var existingCategory=await ci.getByIdAsync(id);
            if(existingCategory == null)
            {
                return NotFound();
                
            }
            var responce = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,

            };
            return Ok(responce);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategory(Guid id,UpdateCategoryRequestDto request)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.urlHandle
            };
            category = await ci.UpadteAsync(category);
            if(category == null)
            {
                return NotFound();
            }
            var responce = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(responce);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category= await ci.deleteAsync(id);
            if(category == null)
            {
                return NotFound();
            }

            var reponse = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(reponse);

        }

    }
}
