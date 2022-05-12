using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Shop.Data.Entities;
using Shop.Domain.Dto;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;


namespace Shop.WebApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
               this.loginService = loginService;
        }

        [Authorize]
        [HttpGet("/getUser")]
        public User GetUser()
        {
            return HttpContext.Items["User"] as User;
        }

        [Authorize]
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var response = await loginService.Register(user);

            if (response == null)
            {
                return BadRequest(new { message = "didn't register!" });
            }

            return Ok(response);
        }

        [HttpPost("/authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var response = loginService.Authenticate(request);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
    }
}
