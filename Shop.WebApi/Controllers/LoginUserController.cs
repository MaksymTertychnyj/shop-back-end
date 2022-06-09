using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Shop.Data.Entities;
using Shop.Domain.Dto;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;


namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("loginEmployee/")]
    public class LoginUserController : ControllerBase
    {
        private readonly ILoginService<User, UserAuthenticateResponse> loginService;

        public LoginUserController(ILoginService<User, UserAuthenticateResponse> loginService)
        {
               this.loginService = loginService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await loginService.Register(user);

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
