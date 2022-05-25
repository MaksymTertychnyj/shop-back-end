using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("product/")]
    public class ProductController : ControllerBase
    {
        private readonly IEntityService<Product> productService;

        public ProductController(IEntityService<Product> productService)
        {
            this.productService = productService;
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            return Ok(await productService.GetAllEntitiesAsync());
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetProductAsync([FromRoute] int id)
        {
            var productObj = await productService.GetEntityByKeyAsync(id);

            if (productObj == null)
                return NotFound();

            return Ok(productObj);
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("add")]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var productObg = await productService.AddEntityAsync(product);

                if (productObg != null)
                    return Ok(product);

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
                    return Ok(product);

                return BadRequest(new {message = "updating the product has been failed"});
            }

            return BadRequest(new {message = "the product doesn't match to entity" });
        }

        [Authorize(Roles = "admin, user")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var productObj = await productService.GetEntityByKeyAsync(id);

            if (productObj != null)
            {
                await productService.DeleteEntityAsync(productObj);
                return Ok();
            }

            return NotFound();
        }
    }
}
