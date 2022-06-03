using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("jsonModel/")]
    public class JsonModelController : ControllerBase
    {
        private readonly IEntityService<JsonModel> modelService;

        public JsonModelController(IEntityService<JsonModel> service)
        {
            modelService = service;
        }


        [HttpGet("getByCategory/{categoryId}")]
        public async Task<IActionResult> GetModelByCategoryAsync([FromRoute] int categoryId)
        {
            var models = await Task.Run(() => modelService.GetEntitiesByPropertyAsync(m => m.CategoryId == categoryId));
            JsonModel model;

            try
            {
                model = models.First();
            }
            catch (Exception)
            {
                return NotFound();
            }

                return Ok(model);
        }


        [Authorize(Roles = "admin")]
        [HttpPost("add/{pattern}/{categoryId}")]
        public async Task<IActionResult> AddModelJsonAsync([FromRoute] string pattern, int categoryId)
        {
            var models = await modelService.GetEntitiesByPropertyAsync(model => model.CategoryId == categoryId);
            JsonModel model;

            try
            {
                model = models.First();
                
                return BadRequest(new { message = "the model already exists" });
            }
            catch (Exception)
            {
                var result = await modelService.AddEntityAsync(new JsonModel() { Pattern = pattern, CategoryId = categoryId });
                if (result != null)
                    return Ok(result);

                return BadRequest(new { message = "adding the model has been failed" });
            }
        }


        [Authorize(Roles = "admin")]
        [HttpPut("update/{pattern}/{categoryId}")]
        public async Task<IActionResult> UpdateModelJsonAsync([FromRoute] string pattern, int categoryId)
        {
            var models = await modelService.GetEntitiesByPropertyAsync(m => m.CategoryId == categoryId);
            JsonModel model;

            try
            {
                model = models.First();
            }
            catch (Exception)
            {
                return NotFound();
            }

            model.Pattern = pattern;
            var result = await modelService.UpdateEntityAsync(model, (int)model.Id);

            if (result != null)
                return Ok(result);

            return BadRequest(new {message = "updating the model has been failed"});
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> DeleteJsonModelAsync([FromRoute] int categoryId)
        {
            var models = await modelService.GetEntitiesByPropertyAsync(m => m.CategoryId == categoryId);
            JsonModel model;

            try
            {
                model = models.First();
            }
            catch (Exception)
            {
                return NotFound();
            }

            await modelService.DeleteEntityAsync(model);
            return Ok();
        }
    }
}
