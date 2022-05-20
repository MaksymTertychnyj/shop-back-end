using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
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

        [Authorize(Roles = "admin, user")]
        [HttpGet("getAllCategories")]
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await categoryService.GetAllEntitiesAsync();
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("getCategory/{key}")]
        public async Task<IActionResult> GetCategory([FromQuery] int key)
        {
            var category = await categoryService.GetEntityByKeyAsync(key);

            if (category != null)
                return Ok(category);

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] Category categoty)
        {
            if (ModelState.IsValid)
            {
                var categoryObj = await categoryService.AddEntityAsync(categoty);

                if (categoryObj != null)
                    return Ok(categoty);
                    

                return BadRequest(new { message = "adding category has been failed" });
            }

            return BadRequest(new {message = "the category doesn't match to entity"});
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteCategory/{key}")]
        public async Task<IActionResult> DeleteCategory([FromQuery] string key)
        {
            Category categoryObj = await categoryService.GetEntityByKeyAsync(int.Parse(key));

            if (categoryObj != null)
            {
                await categoryService.DeleteEntityAsync(categoryObj);
                return Ok();
            }

            return BadRequest(new { message = "the category is not found" });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                var result = await categoryService.UpdateEntityAsync(category, (int)category.Id);

                if (result != null)
                {
                    return Ok();
                }

                return BadRequest(new { message = "updating the category has been failed" });
            }

            return BadRequest(new {message = "the category doesn't match to entity"});
        }
    }
}
