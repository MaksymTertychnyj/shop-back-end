using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("image/")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet("getByParams/{targetId}/{targetType}")]
        public async Task<IActionResult> GetImageAsync(int targetId, int targetType)
        {
            var imageSource = await imageService.RetriveImageAsync(targetId, targetType);

            if (imageSource != null)
                return Ok(imageSource);

            return NotFound();
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("add")]
        public async Task<IActionResult> AddImageAsync()
        {
            if (ModelState.IsValid)
            {
                    var img = await imageService.UpLoadImageAsync(HttpContext.Request.Form);

                    if (img != null)
                        return Ok(img);

                    return BadRequest(new { message = "adding the image has been failed" });
                }

                return BadRequest(new { message = "the image doesn't match to entity" });
        }

        [Authorize(Roles = "admin, user")]
        [HttpDelete("delete/{targetId}/{targetType}")]
        public async Task<IActionResult> DeleteImageAsync(int targetId, int targetType)
        {
            var result = await imageService.DeleteImageAsync(targetId, targetType);

            if (result)
                return Ok();

            return BadRequest(new { message = "deleting the image has been failed" });
        }
    }
}
