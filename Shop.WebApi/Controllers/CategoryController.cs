using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Implementation;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("category/")]
    public class CategoryController : ControllerBase
    {
        private readonly IEntityService<Category> categoryService;
        private readonly ICacheService<Category> cacheCategoryService;

        public CategoryController(
                    IEntityService<Category> categoryService, 
                    ICacheService<Category> cacheCategoryService
                    )
        {
            this.categoryService = categoryService;
            this.cacheCategoryService = cacheCategoryService;
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await cacheCategoryService.GetEntitiesAsync();   
        }

        [HttpGet("getByDepartment/{departmentId}")]
        public async Task<IEnumerable<Category>> GetCategoriesByDepartment([FromRoute] int departmentId)
        {
            return (await cacheCategoryService.GetEntitiesAsync())
                .Where(category => category.DepartmentId == departmentId);              
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("getById/{key}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] int key)
        {
            var category = (await cacheCategoryService.GetEntitiesAsync())
                           .FirstOrDefault(category => category.Id == key);     

            if (category != null)
                return Ok(category);

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add/{name}/{departmentId}")]
        public async Task<IActionResult> AddCategoryAsync([FromRoute] string name, int departmentId)
        {
            if (ModelState.IsValid)
            {
                var categoryObj = await categoryService
                    .AddEntityAsync(new Category { 
                        Name = name, 
                        DepartmentId = departmentId
                    });

                if (categoryObj != null)
                {
                    await cacheCategoryService.UpdateEntitiesAsync();
                    return Ok(categoryObj);
                }

                return BadRequest(new { message = "adding category has been failed" });
            }

            return BadRequest(new {message = "the category doesn't match to entity"});
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{key}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromServices]ICacheService<Product> cacheProductService,  [FromRoute] int key)
        {
            var categoryObj = (await cacheCategoryService.GetEntitiesAsync())
                                     .FirstOrDefault(category => category.Id == key);

            if (categoryObj != null)
            {
                await categoryService.DeleteEntityAsync(categoryObj);
                await cacheCategoryService.UpdateEntitiesAsync();
                await cacheProductService.UpdateEntitiesAsync();
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                var result = await categoryService.UpdateEntityAsync(category, (int)category.Id);

                if (result != null)
                {
                    await cacheCategoryService.UpdateEntitiesAsync();
                    return Ok(result);
                }

                return BadRequest(new { message = "updating the category has been failed" });
            }

            return BadRequest(new {message = "the category doesn't match to entity"});
        }
    }
}
