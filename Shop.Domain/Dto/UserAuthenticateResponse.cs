using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto
{
    public class UserAuthenticateResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; }

        public UserAuthenticateResponse(User user, string token)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Login = user.Login;
            Role = user.Role;
            Token = token;
        }
    }
}
