using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Domain.Dto.CustomerDto;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("customers/")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [Authorize(Roles = "admin, user")]
        [HttpGet("getCustomers")]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerService.GetAllCustomersAsync();
        }


        [Authorize(Roles = "admin, user")]
        [HttpGet("getCustomer/{customerLogin}")]
        public async Task<IActionResult> GetCustomer([FromRoute]string customerLogin)
        {
            var result = await _customerService.GetCustomerByLoginAsync(customerLogin);
            return result != null ? Ok(result) : NotFound();
        }


        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CustomerUpdateDto customerData)
        {
            var customer = (Customer?)HttpContext.Items["User"];

            if (customer != null)
            {
                var result = await _customerService.UpdateCustomerAsync(customer.Login, customerData);
                return Ok(result);
            }

            return BadRequest();
        }


        [Authorize(Roles = "admin, user")]
        [HttpDelete("delete/{customerLogin}")]
        public async Task<IActionResult> Delete([FromRoute]string customerLogin)
        {
            var result = await _customerService.DeleteCustomerAsync(customerLogin);
            return result ? Ok(result) : BadRequest();
        }
    }
}
