using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("employee/")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService service)
        {
            this.employeeService = service;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getAllEmployees")]
        public async Task<IEnumerable<User>> GetAllEmployeesAsync()
        {
            return await employeeService.GetAllEmployeesAsync();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteEmployee/{loginEmployee}")]
        public async Task DeleteEmployee(string loginEmployee)
        {
            User employee = await employeeService.GetEmployeeByLoginAsync(loginEmployee);

            if (employee != null)
            {
                await employeeService.DeleteEmployeeAsync(employee);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("editEmployee")]
        public async Task UpdateEmployee([FromBody] User employee)
        {
            await employeeService.UpdateEmployeeAsync(employee);
        }
    }
}
