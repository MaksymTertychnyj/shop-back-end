using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;
using Shop.Domain.Dto;
using Shop.Domain.Dto.User;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("loginCustomer/")]
    public class LoginCustomerController : ControllerBase
    {
        private readonly ILoginService<Customer, CustomerAuthenticateResponse> loginService;

        public LoginCustomerController(ILoginService<Customer, CustomerAuthenticateResponse> service)
        {
            loginService = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await loginService.Register(customer);

            if (response == null)
            {
                return BadRequest(new { message = "didn't register!" });
            }

            return Ok(response);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "You have not entered your username or password" });
            }

            var response = await loginService.Authenticate(request);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
    }
}
