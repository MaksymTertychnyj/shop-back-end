using Microsoft.AspNetCore.Mvc;
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

        public DepartmentController(IEntityService<Department> departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            return Ok(await departmentService.GetAllEntitiesAsync());
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetDepartmentAsync([FromRoute] int id)
        {
            var department = await departmentService.GetEntityByKeyAsync(id);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add/{name}")]
        public async Task<IActionResult> AddDepartmentAsync([FromRoute] string name)
        {

                var departmentObj = await departmentService.AddEntityAsync(new Department { Name = name});

                if (departmentObj != null)
                    return Ok(departmentObj);

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
                    return Ok(department);

                return BadRequest(new {message = "updating the department has been failed"});
            }

            return BadRequest(new {message = "the department doesn't match to entity"});
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDepartmentAsync([FromRoute] int id)
        {
            var departmentObj = await departmentService.GetEntityByKeyAsync(id);

            if (departmentObj != null)
            {
                await departmentService.DeleteEntityAsync(departmentObj);
                return Ok();
            }

            return NotFound();
        }
    }
}
