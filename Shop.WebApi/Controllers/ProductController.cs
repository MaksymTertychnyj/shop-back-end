using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("product/")]
    public class ProductController : ControllerBase
    {
        private readonly IEntityService<Product> productService;
        private readonly ICacheService<Product> cacheService;

        public ProductController(IEntityService<Product> productService, ICacheService<Product> cacheService)
        {
            this.productService = productService;
            this.cacheService = cacheService;
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await cacheService.GetEntitiesAsync();
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetProductAsync([FromRoute] int id)
        {
            var productObj = (await cacheService.GetEntitiesAsync())
                             .FirstOrDefault(category => category.Id == id);

            if (productObj != null)
                return Ok(productObj);

            return NotFound();
        }

        [HttpGet("getByCategory/{categoryId}")]
        public async Task<IEnumerable<Product>> GetProductByCategoryAsync([FromRoute] int categoryId)
        {
            return (await cacheService.GetEntitiesAsync())
                   .Where(product => product.CategoryId == categoryId);
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("add")]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var productObg = await productService.AddEntityAsync(product);

                if (productObg != null)
                {
                    await cacheService.UpdateEntitiesAsync();
                    return Ok(product);
                }

                return BadRequest(new {message = "adding the product has been failed"});
            }

            return BadRequest(new { message = "the product doesn't match to entity" });
        }

        [Authorize(Roles = "admin, user")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var productObj = await productService.UpdateEntityAsync(product, product.Id);

                if (productObj != null)
                {
                    await cacheService.UpdateEntitiesAsync();
                    return Ok(product);
                }

                return BadRequest(new {message = "updating the product has been failed"});
            }

            return BadRequest(new {message = "the product doesn't match to entity" });
        }

        [Authorize(Roles = "admin, user")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var productObj = (await cacheService.GetEntitiesAsync())
                             .FirstOrDefault(category => category.Id == id);

            if (productObj != null)
            {
                await productService.DeleteEntityAsync(productObj);
                await cacheService.UpdateEntitiesAsync();
                return Ok();
            }

            return NotFound();
        }
    }
}
