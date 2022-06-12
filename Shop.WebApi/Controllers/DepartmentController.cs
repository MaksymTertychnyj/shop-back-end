using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shop.Data.Entities;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("department/")]
    public class DepartmentController : ControllerBase
    {
        private readonly IEntityService<Department> departmentService;
        private readonly ICacheService<Department> cacheDepartmentService;

        public DepartmentController(
                        IEntityService<Department> departmentService, 
                        ICacheService<Department> cacheDepartmentService
                        )
        {
            this.departmentService = departmentService;
            this.cacheDepartmentService = cacheDepartmentService;
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
             return await cacheDepartmentService.GetEntitiesAsync();
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetDepartmentAsync([FromRoute] int id)
        {
            var department = (await cacheDepartmentService.GetEntitiesAsync())
                .FirstOrDefault(d => d.Id == id);

            if (department != null)
                return Ok(department);

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add/{name}")]
        public async Task<IActionResult> AddDepartmentAsync([FromRoute] string name)
        {
                var departmentObj = await departmentService.AddEntityAsync(new Department { Name = name});

                if (departmentObj != null)
                {
                    await cacheDepartmentService.UpdateEntitiesAsync();
                    return Ok(departmentObj);
                }

                return BadRequest(new {message = "adding department has been failed"});
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDepartmentAsync([FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                var departmentObj = await departmentService.UpdateEntityAsync(department, (int)department.Id);

                if (departmentObj != null)
                {
                    await cacheDepartmentService.UpdateEntitiesAsync();
                    return Ok(department);
                }

                return BadRequest(new {message = "updating the department has been failed"});
            }

            return BadRequest(new {message = "the department doesn't match to entity"});
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDepartmentAsync(
                               [FromServices]ICacheService<Category> cacheCategoryService, 
                               [FromServices]ICacheService<Product> cacheProductService, 
                               [FromRoute] int id
                              )
        {
            var departmentObj = (await cacheDepartmentService.GetEntitiesAsync())
                .FirstOrDefault(d => d.Id == id);

            if (departmentObj != null)
            {
                await departmentService.DeleteEntityAsync(departmentObj);
                await cacheDepartmentService.UpdateEntitiesAsync();
                await cacheCategoryService.UpdateEntitiesAsync();
                await cacheProductService.UpdateEntitiesAsync();
                return Ok();
            }

            return NotFound();
        }
    }
}
