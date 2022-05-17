using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("/getUserOne")]
        public string GetUser()
        {
            return "Hello World";
        }
    }
}
