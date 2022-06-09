using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto
{
    public class UserAuthenticateResponse
    {
        public Data.Entities.User User { get; set; }
        public string Token { get; set; }

        public UserAuthenticateResponse(Data.Entities.User user, string token)
        {
            User = user;
            User.Password = "";
            Token = token;
        }
    }
}
