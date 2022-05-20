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

        [Authorize(Roles = "admin, user")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            return Ok(await departmentService.GetAllEntitiesAsync());
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("getById")]
        public async Task<IActionResult> GetDepartmentAsync([FromQuery] int id)
        {
            var department = await departmentService.GetEntityByKeyAsync(id);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                var departmentObg = await departmentService.AddEntityAsync(department);

                if (departmentObg != null)
                    return Ok(department);

                return BadRequest(new {message = "adding department has been failed"});
            }

            return BadRequest(new {message = "the department doesn't match to entity"});
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
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDepartmentAsync([FromQuery] int id)
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
