using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("category/")]
    public class CategoryController : ControllerBase
    {
        private readonly IEntityService<Category> categoryService;

        public CategoryController(IEntityService<Category> entityService)
        {
            categoryService = entityService;
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await categoryService.GetAllEntitiesAsync();
        }

        [HttpGet("getByDepartment/{departmentId}")]
        public async Task<IEnumerable<Category>> GetCategoriesByDepartment([FromRoute] int departmentId)
        {
            return await Task.Run(() => categoryService.GetEntitiesByPropertyAsync(c => c.DepartmentId == departmentId));
        }


        [Authorize(Roles = "admin, user")]
        [HttpGet("getById/{key}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] int key)
        {
            var category = await categoryService.GetEntityByKeyAsync(key);

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
                    return Ok(categoryObj);
                    

                return BadRequest(new { message = "adding category has been failed" });
            }

            return BadRequest(new {message = "the category doesn't match to entity"});
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{key}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int key)
        {
            Category categoryObj = await categoryService.GetEntityByKeyAsync(key);

            if (categoryObj != null)
            {
                await categoryService.DeleteEntityAsync(categoryObj);
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
                    return Ok(result);
                }

                return BadRequest(new { message = "updating the category has been failed" });
            }

            return BadRequest(new {message = "the category doesn't match to entity"});
        }
    }
}
